using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wynajme_AspNetCore_v2.Models.AdminViewModels;
using Wynajme_AspNetCore_v2.Repository;

namespace Wynajme_AspNetCore_v2.Controllers
{
    public class AdminController : Controller
    {
        private readonly IOgloszenieRepository _ogloszenieRepo;
        private readonly IManageRepository _manageRepo;

        public AdminController (IOgloszenieRepository ogloszenieRepo, IManageRepository manageRepo)
        {
            _ogloszenieRepo = ogloszenieRepo;
            _manageRepo = manageRepo;
        }

        public IActionResult Index()
        {
            var model = new AdminIndexViewModel
            {
                UsersCount = _manageRepo.GetUsers().Count(),
                AdminsCount = _manageRepo.GetAdmins().Count(),
                KategorieCount = _ogloszenieRepo.GetKategorie().Count(),
                LokalizacjeCount = _ogloszenieRepo.GetMiasta().Count(),
                OgloszeniaCount = _ogloszenieRepo.GetOgloszenia().Count()
            };

            return View(model);
        }

        public IActionResult Users()
        {
            var model = new UsersViewModel
            {
                Users = _manageRepo.GetUsers()
            };

            return View(model);
        }
    }
}