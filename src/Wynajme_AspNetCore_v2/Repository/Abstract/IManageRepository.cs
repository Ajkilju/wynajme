using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wynajme_AspNetCore_v2.Models;

namespace Wynajme_AspNetCore_v2.Repository
{
    public interface IManageRepository
    {
        Task<ApplicationUser> PobierzUzytkownika(string Id, DaneUzytkownika dane, TrackingManage tracking);


        bool IsAnyUserRegistered();
        int GetRegisterdeUsersCount();
        IQueryable<ApplicationUser> GetUsers();
        IQueryable<ApplicationUser> GetAdmins();

        void DeleteUser(ApplicationUser user);
        void UpdateUser(ApplicationUser user);
        void SaveChanges();
    }
}
