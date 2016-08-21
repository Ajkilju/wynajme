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
using System.IO;

namespace Wynajme_AspNetCore_v2.Controllers
{
    public class OgloszenieController : Controller
    {
        private readonly IOgloszenieRepository _ogloszenieRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private IManageRepository _manageRepo;

        public OgloszenieController (
            UserManager<ApplicationUser> userManager, 
            IOgloszenieRepository ogloszenieRepo,
            IManageRepository manageRepo)
        {
            _userManager = userManager;
            _ogloszenieRepo = ogloszenieRepo;
            _manageRepo = manageRepo;
        }

        // GET: Ogloszenies
        //[ResponseCache(Location = ResponseCacheLocation.Client, Duration = 120)]
        public async Task<IActionResult> Index(
            int? cenaOd, 
            int? cenaDo, 
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
                Ogloszenia = await _ogloszenieRepo.PobierzOgloszenia(
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
                    .ToPagedListAsync(pageSize, page),

                Kategorie = await _ogloszenieRepo.PobierzKategorieAsync(),
                Miasta = await _ogloszenieRepo.PobierzMiastaAsync(),
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
        public async Task<IActionResult> Details(int? id)
        {
            var ogloszenie = await _ogloszenieRepo.PobierzOgloszenieAsync(id, 
                DaneOgloszenia.Wszystko, TrackingOgloszenia.AsNoTracking);
            var loginUserId = _userManager.GetUserId(HttpContext.User); 

            OgloszenieDetailsViewModel model = new OgloszenieDetailsViewModel
            {
                Ogloszenie = ogloszenie,
                PodobneOgloszenia = await _ogloszenieRepo.PobierzPodobneOgloszenia(3,ogloszenie),
                LogInUserId = loginUserId,
                Obserwowane = false
            };

            if (loginUserId != null)
            {
                var loginUser = await _manageRepo.PobierzUzytkownikaAsync(loginUserId,
                    DaneUzytkownika.Wszystko, TrackingManage.AsNoTracking);

                foreach (var item in loginUser.Obserwowane)
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


        public async Task<IActionResult> Create()
        {
            var model = new OgloszenieCreateViewModel
            {
                Ogloszenie = new Ogloszenie
                {
                    Tytul = "Tytu³ og³oszenia...",
                    Tresc = "Treœæ og³oszenia..."
                },
                KategorieId = new SelectList(await _ogloszenieRepo.PobierzKategorieAsync(), "KategoriaId", "Nazwa"),
                MiastaId = new SelectList(await _ogloszenieRepo.PobierzMiastaAsync(), "MiastoId", "Nazwa")
            };

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if(user != null)
            {
                model.LoggedUser = await _manageRepo.PobierzUzytkownikaAsync(user.Id,
                    DaneUzytkownika.Wszystko, TrackingManage.Tracking);
                model.Ogloszenie.UserId = model.LoggedUser.Id;
            }
            else
            {
                model.LoggedUser = null;
            }

            return View(model);
        }

        // POST: Ogloszenies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OgloszenieCreateViewModel model, IList<IFormFile> images)
        {
            if (ModelState.IsValid)
            {
                _ogloszenieRepo.DodajOgloszenie(model.Ogloszenie, images);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Ogloszenies/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            var model = new OgloszenieCreateViewModel
            {
                Ogloszenie = await _ogloszenieRepo.PobierzOgloszenieAsync(id,
                    DaneOgloszenia.Wszystko, TrackingOgloszenia.Tracking),
                KategorieId = new SelectList(await _ogloszenieRepo.PobierzKategorieAsync(), "KategoriaId", "Nazwa"),
                MiastaId = new SelectList(await _ogloszenieRepo.PobierzMiastaAsync(), "MiastoId", "Nazwa")
            };

            return View(model);
        }

        // POST: Ogloszenies/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OgloszenieCreateViewModel model, IList<IFormFile> images)
        {
            if (ModelState.IsValid)
            {
                var ogloszenie = await _ogloszenieRepo.PobierzOgloszenieAsync(model.Ogloszenie.OgloszenieId,
                    DaneOgloszenia.Wszystko, TrackingOgloszenia.AsNoTracking);

                model.Ogloszenie.DataDodania = ogloszenie.DataDodania;
                if(images.Count == 0)
                {
                    model.Ogloszenie.Miniature = ogloszenie.Miniature;
                    model.Ogloszenie.Image = ogloszenie.Image;
                }
                _ogloszenieRepo.AktualizujOgloszenie(model.Ogloszenie, images);
                return RedirectToAction("Index");
            }          
            return View(model);
        }

        // GET: Ogloszenies/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            Ogloszenie ogloszenie = await _ogloszenieRepo.PobierzOgloszenieAsync(id,
                DaneOgloszenia.Wszystko, TrackingOgloszenia.Tracking);
            return View(ogloszenie);
        }

        // POST: Ogloszenies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _ogloszenieRepo.UsunOgloszenie(id);
            return RedirectToAction("Index");
        }

        // POST: Ogloszenies/Obserwuj
        //[HttpPost]
        public async Task<IActionResult> Obserwuj(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var repoUser = await _manageRepo.PobierzUzytkownikaAsync(user.Id, 
                DaneUzytkownika.Wszystko, TrackingManage.Tracking);

            if (user != null)
            {
                Obserwowane obs = new Obserwowane { OgloszenieId = id };

                if (repoUser.Obserwowane.Count == 0)
                    repoUser.Obserwowane = new List<Obserwowane>();
                repoUser.Obserwowane.Add(obs);
                _manageRepo.AktualizujUzytkownika(repoUser);
                _manageRepo.SaveChanges();
                return RedirectToAction("Details", new { id = id } );
            }
            return RedirectToAction("Index");
        }

        // POST: Ogloszenies/Obserwuj
        //[HttpPost]
        public async Task<IActionResult> NieObserwuj(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var repoUser = await _manageRepo.PobierzUzytkownikaAsync(user.Id,
                DaneUzytkownika.Wszystko, TrackingManage.Tracking);

            if (user != null)
            {
                _ogloszenieRepo.NieObserwuj(id);
                return RedirectToAction("Obserwowane","Manage");
            }
            return RedirectToAction("Index");
        }
    }
}
