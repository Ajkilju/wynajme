using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wynajme_AspNetCore_v2.Data;
using Wynajme_AspNetCore_v2.Models;

namespace Wynajme_AspNetCore_v2.Repository
{
    public enum TrackingOgloszenia
    {
        AsNoTracking,
        Tracking
    }

    public enum DaneOgloszenia
    {
        Wszystko,
        Podstawowe
    }

    public class OgloszenieRepository : IOgloszenieRepository
    {
        private ApplicationDbContext _context;
        
        public OgloszenieRepository(ApplicationDbContext context)
        {            
            _context = context;           
        }

        public IQueryable<Ogloszenie> PobierzOgloszenia (
            string miasto,
            string kategoria,
            string searchString, 
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
            string UserId,
            int? howMany = null)
        {
            if (cenaOd == null) cenaOd = 0;
            if (cenaDo == null) cenaDo = int.MaxValue;

            var ogloszenia = _context.Ogloszenie
                .Include(o => o.Kategoria)
                .Include(o => o.Miasto)
                .Where(c => c.Cena >= cenaOd && c.Cena <= cenaDo);

            if (UserId != null) 
            {
                ogloszenia = ogloszenia .Where(u => u.UserId == UserId);
            }

            if (!String.IsNullOrEmpty(kategoria))
            {
                ogloszenia = ogloszenia.Where(s => s.Kategoria.Nazwa == kategoria);
            }

            if (!String.IsNullOrEmpty(miasto))
            {
                ogloszenia = ogloszenia.Where(s => s.Miasto.Nazwa == miasto);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                ogloszenia = ogloszenia.Where(s => s.Tytul.Contains(searchString));
            }

            if (zmywarka == true)
            {
                ogloszenia = ogloszenia.Where(w => w.Zmywarka == true);
            }

            if (lodowka == true)
            {
                ogloszenia = ogloszenia.Where(w => w.Lodowka == true);
            }

            if (mikrofala == true)
            {
                ogloszenia = ogloszenia.Where(w => w.Mikrofala == true);
            }

            if (kuchniaGaz == true)
            {
                ogloszenia = ogloszenia.Where(w => w.KuchniaGaz == true);
            }

            if (kuchniaEl == true)
            {
                ogloszenia = ogloszenia.Where(w => w.KuchniaEl == true);
            }

            if (pralka == true)
            {
                ogloszenia = ogloszenia.Where(w => w.Pralka == true);
            }

            if (wanna == true)
            {
                ogloszenia = ogloszenia.Where(w => w.Wanna == true);
            }

            if (prysznic == true)
            {
                ogloszenia = ogloszenia.Where(w => w.Prysznic == true);
            }

            switch (sortOrder)
            {
                case 0:
                    ogloszenia = ogloszenia.OrderByDescending(o => o.DataDodania);
                    break;
                case 1:
                    ogloszenia = ogloszenia.OrderBy(o => o.DataDodania);
                    break;
                case 2:
                    ogloszenia = ogloszenia.OrderByDescending(o => o.Cena);
                    break;
                case 3:
                    ogloszenia = ogloszenia.OrderBy(o => o.Cena);
                    break;
                default:
                    ogloszenia = ogloszenia.OrderByDescending(o => o.DataDodania);
                    break;
            }

            if(howMany != null)
            {
                ogloszenia = ogloszenia.Take((int)howMany);
            }
            return ogloszenia.AsNoTracking();
        }

        public IQueryable<Ogloszenie> PobierzOgloszenia(int howMany)
        {
            return _context.Ogloszenie
                .Include(m => m.Miasto)
                .Include(k => k.Kategoria)
                .OrderByDescending(o => o.DataDodania)
                .AsNoTracking()
                .Take(howMany);
        }

        public IQueryable<Ogloszenie> PobierzOgloszenia()
        {
            return _context.Ogloszenie.AsNoTracking();
        }

        public async Task<List<Ogloszenie>> PobierzOgloszeniaAsync(int howMany)
        {
            return await PobierzOgloszenia(howMany).ToListAsync();
        }

        public async Task<List<Ogloszenie>> PobierzOgloszeniaAsync()
        {
            return await _context.Ogloszenie.ToListAsync();
        }

        public async Task<Ogloszenie> PobierzOgloszenieAsync(int? id, DaneOgloszenia dane, TrackingOgloszenia tracking)
        {
            if(dane == DaneOgloszenia.Podstawowe)
            {
                var ogl = _context.Ogloszenie
                .Include(o => o.Kategoria)
                .Include(o => o.Miasto);

                if(tracking == TrackingOgloszenia.AsNoTracking)
                {
                    return await ogl.AsNoTracking()
                        .SingleAsync(m => m.OgloszenieId == id);
                }
                else
                {
                    return await ogl.SingleAsync(m => m.OgloszenieId == id);
                }
            }
            else 
            {
                var ogl = _context.Ogloszenie
                .Include(o => o.Kategoria)
                .Include(o => o.Miasto)
                .Include(o => o.Image)
                .Include(o => o.User);

                if (tracking == TrackingOgloszenia.AsNoTracking)
                {
                    return await ogl.AsNoTracking()
                        .SingleAsync(m => m.OgloszenieId == id);
                }
                else
                {
                    return await ogl.SingleAsync(m => m.OgloszenieId == id);
                }
            }                
        }

        public async Task<List<Ogloszenie>> PobierzPodobneOgloszenia(int howMany, Ogloszenie ogloszenie)
        {
            return await _context.Ogloszenie
                .Include(m => m.Miasto)
                .Include(k => k.Kategoria)
                .Where(s => s.Kategoria.Nazwa == ogloszenie.Kategoria.Nazwa)
                .Where(s => s.Miasto.Nazwa == ogloszenie.Miasto.Nazwa)
                .Where(s => s.OgloszenieId != ogloszenie.OgloszenieId)
                .OrderByDescending(o => o.DataDodania)
                .AsNoTracking()
                .Take(howMany)
                .ToListAsync();
        }

        public IQueryable<Kategoria> PobierzKategorie()
        {
            return _context.Kategoria.AsNoTracking();
        }

        public async Task<List<Kategoria>> PobierzKategorieAsync()
        {
            return await _context.Kategoria.ToListAsync();
        }

        public IQueryable<Miasto> PobierzMiasta()
        {
            return _context.Miasto.AsNoTracking();
        }

        public async Task<List<Miasto>> PobierzMiastaAsync()
        {
            return await _context.Miasto.ToListAsync();
        }

        public void DodajOgloszenie(Ogloszenie ogloszenie, IList<IFormFile> images)
        {
            ogloszenie.DataDodania = DateTime.Now;

            if (images.Count > 0)
            {
                int imagesCount = images.Count;
                ogloszenie.Image = new List<Image>();
                Image[] img = new Image[imagesCount];

                for (int i = 0; i < imagesCount; i++)
                {
                    using (var reader = new BinaryReader(images[i].OpenReadStream()))
                    {
                        var fileContent = reader.ReadBytes((int)images[i].Length);
                        img[i] = new Image();
                        img[i].ImageContent = fileContent;
                    }
                    ogloszenie.Image.Add(img[i]);
                }
                ogloszenie.Miniature = img[0].ImageContent;
            }
            _context.Ogloszenie.Add(ogloszenie);
            _context.SaveChanges();
        }

        public void AktualizujOgloszenie(Ogloszenie ogloszenie, IList<IFormFile> images)
        {
            if (images.Count > 0)
            {
                int imagesCount = images.Count;
                ogloszenie.Image = new List<Image>();
                Image[] img = new Image[imagesCount];

                for (int i = 0; i < imagesCount; i++)
                {
                    using (var reader = new BinaryReader(images[i].OpenReadStream()))
                    {
                        var fileContent = reader.ReadBytes((int)images[i].Length);
                        img[i] = new Image();
                        img[i].ImageContent = fileContent;
                    }
                    ogloszenie.Image.Add(img[i]);
                }
                ogloszenie.Miniature = img[0].ImageContent;
            }
            _context.Update(ogloszenie);
            _context.SaveChanges();
        }

        public void UsunOgloszenie(int? id)
        {
            Ogloszenie ogloszenie = _context.Ogloszenie.Single(m => m.OgloszenieId == id);
            _context.Ogloszenie.Remove(ogloszenie);
            _context.SaveChanges();
        }

        public void NieObserwuj(int? id)
        {
            Obserwowane obs = _context.Obserwowane.Single(m => m.Id == id);
            _context.Obserwowane.Remove(obs);
            _context.SaveChanges();
        }

        public void SaveChages()
        {
            _context.SaveChanges();
        }
    }
}
