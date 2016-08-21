using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wynajme_AspNetCore_v2.Models;

namespace Wynajme_AspNetCore_v2.Repository
{
    public interface IManageRepository
    {
        Task<ApplicationUser> PobierzUzytkownikaAsync(string Id, DaneUzytkownika dane, TrackingManage tracking);
        void AktualizujUzytkownika(ApplicationUser user);
        int LiczbaZarejestrowanychUzytkownikow();
        IQueryable<ApplicationUser> PobierzUzytkownikow();
        IQueryable<ApplicationUser> PobierzAdministratorow();
        void UsunUzytkownika(ApplicationUser user);
        void SaveChanges();
    }
}
