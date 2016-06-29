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
            string UserId);

        Ogloszenie GetOgloszenie(int? id);
        Ogloszenie GetNakedOgloszenie(int? id);
        Ogloszenie GetOgloszenieAsNoTracking(int? id);

        IQueryable<Ogloszenie> GetOgloszenia(int howMany);
        IQueryable<Ogloszenie> GetOgloszenia(int howMany, string kategoria, string miasto);
        IQueryable<Ogloszenie> GetSimmlarOgloszenia(int howMany, Ogloszenie ogloszenie);

        void DodajOgloszenie(Ogloszenie ogloszenie, IList<IFormFile> images);
        void UpdateOgloszenie(Ogloszenie ogloszenie);
        void DeleteOgloszenie(int? id);
        void SaveChages();

        IQueryable<Kategoria> GetKategorie();
        IQueryable<Miasto> GetMiasta();
    }
}
