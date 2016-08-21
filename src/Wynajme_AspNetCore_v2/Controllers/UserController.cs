using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wynajme_AspNetCore_v2.Data;
using Microsoft.EntityFrameworkCore;
using Wynajme_AspNetCore_v2.Models.UserViewModel;
using Wynajme_AspNetCore_v2.Repository;
using Microsoft.AspNetCore.Identity;
using Wynajme_AspNetCore_v2.Models;

namespace Wynajme_AspNetCore_v2.Controllers
{
    public class UserController : Controller
    {
        private IManageRepository _managerRepo;

        public UserController(IManageRepository managerRepo)
        {
            _managerRepo = managerRepo;
        }

        public async Task<IActionResult> Index(string UserId, int page = 1)
        {
            if (page == 0) page = 1;
            var user = await _managerRepo.PobierzUzytkownikaAsync(UserId,
                    DaneUzytkownika.Wszystko, TrackingManage.Tracking);
            var model = new UserIndexViewModel(user, page);
            return View(model);
        }

        public async Task<IActionResult> Delete(string UserId)
        {
            var user = await _managerRepo.PobierzUzytkownikaAsync(UserId,
                    DaneUzytkownika.Wszystko, TrackingManage.Tracking);
            _managerRepo.UsunUzytkownika(user);
            _managerRepo.SaveChanges();
            return RedirectToAction("Users", "Admin");
        }
    }
}