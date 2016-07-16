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
using Microsoft.AspNetCore.Authorization;

namespace Wynajme_AspNetCore_v2.Controllers
{
    public class OgloszenieController : Controller
    {
        private readonly IOgloszenieRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private IManageRepository _managerRepo;

        public OgloszenieController (
            UserManager<ApplicationUser> userManager, 
            IOgloszenieRepository repository,
            IManageRepository managerRepo)
        {
            _userManager = userManager;
            _repository = repository;
            _managerRepo = managerRepo;
        }

        // GET: Ogloszenies
        //[ResponseCache(Location = ResponseCacheLocation.Client, Duration = 120)]
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
            if (page == 0) page = 1;

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
                SearchString = searchString == null ? "" : searchString,
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
                    "Data malejaco","Data rosnaco","Cena malejaco","Cena rosnaco"
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
        //[ResponseCache(Location = ResponseCacheLocation.Client, Duration = 120)]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                //return HttpNotFound();
            }

            var ogloszenie = _repository.GetOgloszenieAsNoTracking(id);

            if (ogloszenie == null)
            {
                // return HttpNotFound();
            }

            OgloszenieDetailsViewModel model = new OgloszenieDetailsViewModel(ogloszenie);
            model.SetSimmlarOgloszenia(_repository.GetSimmlarOgloszenia(3, ogloszenie));

            var logInUserId = _userManager.GetUserId(HttpContext.User);
            model.LogInUserId = logInUserId;

            if (logInUserId != null)
            {
                var user = _managerRepo.GetUserAllData(logInUserId);
                foreach (var item in user.Obserwowane)
                {
                    if (item.OgloszenieId == ogloszenie.OgloszenieId)
                    {
                        model.Obserwowane = true;
                        break;
                    }
                }
            } 

            return View(model);
        }

        // GET: Ogloszenies/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {
                ViewData["IsUserLoggIn"] = 1;
                ViewData["UserEmail"] = user.Email;
                ViewData["UserPhone"] = user.PhoneNumber;
            }
            else
            {
                ViewData["IsUserLoggIn"] = 0;
                ViewData["UserEmail"] = "";
                ViewData["UserPhone"] = "";
            }

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
        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
               // return HttpNotFound();
            }

            Ogloszenie ogloszenie = _repository.GetOgloszenie(id);

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
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Ogloszenie ogloszenie)
        {
            if (ModelState.IsValid)
            {
                ogloszenie.DataDodania = DateTime.Now;
                ogloszenie.UserId = _userManager.GetUserId(HttpContext.User);
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

            Ogloszenie ogloszenie = _repository.GetOgloszenie(id);
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

        // POST: Ogloszenies/Obserwuj
        //[HttpPost]
        public async Task<IActionResult> Obserwuj(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var repoUser = _managerRepo.GetUserAllData(user.Id);

            if (user != null)
            {
                Obserwowane obs = new Obserwowane
                {
                    Ogloszenie = _repository.GetOgloszenie(id),
                };

                if (repoUser.Obserwowane.Count == 0) repoUser.Obserwowane = new List<Obserwowane>();
                repoUser.Obserwowane.Add(obs);
                _repository.SaveChages();
                _managerRepo.UpdateUser(repoUser);
                _managerRepo.SaveChanges();

                return RedirectToAction("Details", new { id = id } );
            }

            return RedirectToAction("Index");
        }

        // POST: Ogloszenies/Obserwuj
        //[HttpPost]
        public async Task<IActionResult> NieObserwuj(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var repoUser = _managerRepo.GetUserAllData(user.Id);

            if (user != null)
            {

                _repository.NieObserwuj(id);

                /*
                repoUser.Obserwowane.Remove()
                _repository.SaveChages();
                _managerRepo.UpdateUser(repoUser);
                */

                return RedirectToAction("Obserwowane","Manage");
            }

            return RedirectToAction("Index");
        }

    }
}
