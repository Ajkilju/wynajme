using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wynajme_AspNetCore_v2.Models;

namespace Wynajme_AspNetCore_v2.Repository
{
    public interface IOgloszenieRepository
    {
        IQueryable<Ogloszenie> PobierzOgloszenia(
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
            int? howMany = null);

        IQueryable<Ogloszenie> PobierzOgloszenia(int howMany);
        IQueryable<Ogloszenie> PobierzOgloszenia();
        Task<List<Ogloszenie>> PobierzOgloszeniaAsync(int howMany);
        Task<List<Ogloszenie>> PobierzOgloszeniaAsync();
        Task<Ogloszenie> PobierzOgloszenieAsync(int? id, DaneOgloszenia dane, TrackingOgloszenia tracking);
        Task<List<Ogloszenie>> PobierzPodobneOgloszenia(int howMany, Ogloszenie ogloszenie);

        IQueryable<Kategoria> PobierzKategorie();
        Task<List<Kategoria>> PobierzKategorieAsync();

        IQueryable<Miasto> PobierzMiasta();
        Task<List<Miasto>> PobierzMiastaAsync();

        void DodajOgloszenie(Ogloszenie ogloszenie, IList<IFormFile> images);
        void AktualizujOgloszenie(Ogloszenie ogloszenie, IList<IFormFile> images);
        void UsunOgloszenie(int? id);
        void NieObserwuj(int? id);
        void SaveChages();          
          
    }
}
