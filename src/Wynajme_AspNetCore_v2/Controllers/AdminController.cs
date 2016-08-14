using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wynajme_AspNetCore_v2.Models.AdminViewModels;
using Wynajme_AspNetCore_v2.Repository;
using Microsoft.AspNetCore.Authorization;

namespace Wynajme_AspNetCore_v2.Controllers
{
    [Authorize(Roles = "Admin")]
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
                KategorieCount = _ogloszenieRepo.PobierzKategorie().Count(),
                LokalizacjeCount = _ogloszenieRepo.PobierzMiasta().Count(),
                OgloszeniaCount = _ogloszenieRepo.PobierzOgloszenia().Count()
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