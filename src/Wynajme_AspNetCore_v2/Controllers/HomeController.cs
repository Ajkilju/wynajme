using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wynajme_AspNetCore_v2.Data;
using Wynajme_AspNetCore_v2.Models.HomeViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Wynajme_AspNetCore_v2.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        { 

            NewHomeViewModel viewModel = new NewHomeViewModel
            {
                Kategorie = _context.Kategoria,
                Miasta = _context.Miasto,
                Ogloszenia = _context.Ogloszenie
                             .Include(m => m.Miasto)
                             .Include(k => k.Kategoria)
                             .OrderByDescending(o => o.DataDodania)
                             .AsNoTracking()
                             .Take(9)
            };

            ViewData["kategoria"] = new SelectList(_context.Kategoria, "Nazwa", "Nazwa");
            ViewData["miasto"] = new SelectList(_context.Miasto, "Nazwa", "Nazwa");

            return View(viewModel);
        }
    }
}