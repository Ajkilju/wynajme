using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wynajme_AspNetCore_v2.Data;
using Wynajme_AspNetCore_v2.Models;
using Wynajme_AspNetCore_v2.Repository;
using Wynajme_AspNetCore_v2.Models.OgloszenieViewModel;
using Microsoft.AspNetCore.Http;
using Sakura.AspNetCore;
using Microsoft.AspNetCore.Identity;

namespace Wynajme_AspNetCore_v2.Controllers
{
    public class OgloszenieController : Controller
    {
        private readonly IOgloszenieRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public OgloszenieController(UserManager<ApplicationUser> userManager, IOgloszenieRepository repository)
        {
            _userManager = userManager;
            _repository = repository;
        }

        // GET: Ogloszenies
        public IActionResult Index(
            int? cenaOd, int? cenaDo, 
            int? sortOrder, 
            Boolean? zmywarka,
            Boolean? lodowka,
            Boolean? mikrofala, 
            Boolean? kuchniaGaz,
            Boolean? kuchniaEl,
            Boolean? pralka,
            Boolean? wanna,
            Boolean? prysznic,
            string kategoria, 
            string miasto, 
            string searchString, 
            int pageSize, 
            int page = 1, 
            string UserId = null)
        {
            if (pageSize < 10) pageSize = 10;

            OgloszenieIndexViewModel model = new OgloszenieIndexViewModel
            {
                Ogloszenia = _repository.PobierzOgloszenia(
                    miasto, 
                    kategoria, 
                    searchString, 
                    cenaOd, 
                    cenaDo, 
                    sortOrder,
                    zmywarka,
                    lodowka,
                    mikrofala,
                    kuchniaGaz,
                    kuchniaEl,
                    pralka,
                    wanna,
                    prysznic,
                    UserId)
                    .ToPagedList(pageSize, page),

                Kategorie = _repository.GetKategorie(),
                Miasta = _repository.GetMiasta(),
                AktualnaKategoria = kategoria,
                AktualneMiasto = miasto,
                SearchString = searchString == null ? "Szukaj..." : searchString,
                SearchStringRoute = searchString,
                CenaOdList = new List<int?>
                {
                    0,100,200,300,400,500,600,700,800,900,1000,
                    1100,1200,1300,1400,1500,1600,1700,1800,1900,2000
                },
                CenaOd = cenaOd,
                CenaDoList = new List<int?>
                {
                    100,200,300,400,500,600,700,800,900,1000,
                    1100,1200,1300,1400,1500,1600,1700,1800,1900,2000
                },
                CenaDo = cenaDo,
                SortOrderList = new List<string>
                {
                    "Data malejaco","Data rosnaco", "Cena malejaco","Cena rosnaco"
                },
                SortOrderRoute = sortOrder,
                PageSizeList = new List<int>
                {
                    10,20,50
                },
                PageSize = pageSize,

                Zmywarka = zmywarka,
                Lodowka = lodowka,
                Mikrofala = mikrofala,
                KuchniaGaz = kuchniaGaz,
                KuchniaEl = kuchniaEl,
                Pralka = pralka,
                Wanna = wanna,
                Prysznic = prysznic,
            };
            model.SortOrder = sortOrder == null ? model.SortOrderList[0] : model.SortOrderList[(int)sortOrder];

            return View(model);
        }

        // GET: Ogloszenies/Details/5

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                //return HttpNotFound();
            }

            Ogloszenie ogloszenie = _repository.GetOgloszenieById(id);

            if (ogloszenie == null)
            {
                //return HttpNotFound();
            }

            ViewData["Podobne"] = _repository.GetSimmlarOgloszenia(4, ogloszenie);

            return View(ogloszenie);
        }

        // GET: Ogloszenies/Create
        public IActionResult Create()
        {
            ViewData["KategoriaId"] = new SelectList(_repository.GetKategorie(), "KategoriaId", "Nazwa");
            ViewData["MiastoId"] = new SelectList(_repository.GetMiasta(), "MiastoId", "Nazwa");

            return View();
        }

        // POST: Ogloszenies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Ogloszenie ogloszenie, IList<IFormFile> images)
        {
            if (ModelState.IsValid)
            {
               ogloszenie.UserId  = _userManager.GetUserId(HttpContext.User);
                _repository.DodajOgloszenie(ogloszenie, images);
                return RedirectToAction("Index");
            }
            ViewData["KategoriaId"] = new SelectList(_repository.GetKategorie(), "KategoriaId", "Nazwa", ogloszenie.KategoriaId);
            ViewData["MiastoId"] = new SelectList(_repository.GetMiasta(), "MiastoId", "Nazwa", ogloszenie.MiastoId);
            return View(ogloszenie);
        }

        // GET: Ogloszenies/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
               // return HttpNotFound();
            }

            Ogloszenie ogloszenie = _repository.GetOgloszenieById(id);

            if (ogloszenie == null)
            {
               // return HttpNotFound();
            }

            ViewData["KategoriaId"] = new SelectList(_repository.GetKategorie(), "KategoriaId", "Nazwa", ogloszenie.KategoriaId);
            ViewData["MiastoId"] = new SelectList(_repository.GetMiasta(), "MiastoId", "Nazwa", ogloszenie.MiastoId);

            return View(ogloszenie);
        }

        // POST: Ogloszenies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Ogloszenie ogloszenie)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdateOgloszenie(ogloszenie);
                return RedirectToAction("Index");
            }
            ViewData["KategoriaId"] = new SelectList(_repository.GetKategorie(), "KategoriaId", "Nazwa", ogloszenie.KategoriaId);
            ViewData["MiastoId"] = new SelectList(_repository.GetMiasta(), "MiastoId", "Nazwa", ogloszenie.MiastoId);
            return View(ogloszenie);
        }

        // GET: Ogloszenies/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
               // return HttpNotFound();
            }

            Ogloszenie ogloszenie = _repository.GetNakedOgloszenieById(id);
            if (ogloszenie == null)
            {
               // return HttpNotFound();
            }

            return View(ogloszenie);
        }

        // POST: Ogloszenies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteOgloszenie(id);
            return RedirectToAction("Index");
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }


        /*
        // GET: Ogloszenie
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ogloszenie.Include(o => o.Kategoria).Include(o => o.Miasto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Ogloszenie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogloszenie = await _context.Ogloszenie.SingleOrDefaultAsync(m => m.OgloszenieId == id);
            if (ogloszenie == null)
            {
                return NotFound();
            }

            return View(ogloszenie);
        }

        // GET: Ogloszenie/Create
        public IActionResult Create()
        {
            ViewData["KategoriaId"] = new SelectList(_context.Set<Kategoria>(), "KategoriaId", "Kategoria");
            ViewData["MiastoId"] = new SelectList(_context.Set<Miasto>(), "MiastoId", "Miasto");
            return View();
        }

        // POST: Ogloszenie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OgloszenieId,Cena,DataDodania,KategoriaId,MiastoId,Miniature,MiniatureMimeType,Tresc,Tytul")] Ogloszenie ogloszenie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ogloszenie);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["KategoriaId"] = new SelectList(_context.Set<Kategoria>(), "KategoriaId", "Kategoria", ogloszenie.KategoriaId);
            ViewData["MiastoId"] = new SelectList(_context.Set<Miasto>(), "MiastoId", "Miasto", ogloszenie.MiastoId);
            return View(ogloszenie);
        }

        // GET: Ogloszenie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogloszenie = await _context.Ogloszenie.SingleOrDefaultAsync(m => m.OgloszenieId == id);
            if (ogloszenie == null)
            {
                return NotFound();
            }
            ViewData["KategoriaId"] = new SelectList(_context.Set<Kategoria>(), "KategoriaId", "Kategoria", ogloszenie.KategoriaId);
            ViewData["MiastoId"] = new SelectList(_context.Set<Miasto>(), "MiastoId", "Miasto", ogloszenie.MiastoId);
            return View(ogloszenie);
        }

        // POST: Ogloszenie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OgloszenieId,Cena,DataDodania,KategoriaId,MiastoId,Miniature,MiniatureMimeType,Tresc,Tytul")] Ogloszenie ogloszenie)
        {
            if (id != ogloszenie.OgloszenieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ogloszenie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OgloszenieExists(ogloszenie.OgloszenieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["KategoriaId"] = new SelectList(_context.Set<Kategoria>(), "KategoriaId", "Kategoria", ogloszenie.KategoriaId);
            ViewData["MiastoId"] = new SelectList(_context.Set<Miasto>(), "MiastoId", "Miasto", ogloszenie.MiastoId);
            return View(ogloszenie);
        }

        // GET: Ogloszenie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogloszenie = await _context.Ogloszenie.SingleOrDefaultAsync(m => m.OgloszenieId == id);
            if (ogloszenie == null)
            {
                return NotFound();
            }

            return View(ogloszenie);
        }

        // POST: Ogloszenie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ogloszenie = await _context.Ogloszenie.SingleOrDefaultAsync(m => m.OgloszenieId == id);
            _context.Ogloszenie.Remove(ogloszenie);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool OgloszenieExists(int id)
        {
            return _context.Ogloszenie.Any(e => e.OgloszenieId == id);
        }
        */
    }
}
