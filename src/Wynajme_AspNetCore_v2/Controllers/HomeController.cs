using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wynajme_AspNetCore_v2.Data;
using Wynajme_AspNetCore_v2.Models.HomeViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wynajme_AspNetCore_v2.Repository;

namespace Wynajme_AspNetCore_v2.Controllers
{
    public class HomeController : Controller
    {
        private IOgloszenieRepository _ogloszenieRepo;

        public HomeController(IOgloszenieRepository ogloszenieRepo)
        {
            _ogloszenieRepo = ogloszenieRepo;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            NewHomeViewModel model = new NewHomeViewModel
            {
                Kategorie = await _ogloszenieRepo.PobierzKategorieAsync(),
                Miasta = await _ogloszenieRepo.PobierzMiastaAsync(),
                Ogloszenia = await _ogloszenieRepo.PobierzOgloszeniaAsync(9)
            };
            return View(model);
        }
    }
}