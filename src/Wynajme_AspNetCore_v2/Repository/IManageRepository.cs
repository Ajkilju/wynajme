using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wynajme_AspNetCore_v2.Models;

namespace Wynajme_AspNetCore_v2.Repository
{
    public interface IManageRepository
    {
        bool IsAnyUserRegistered();
        int GetRegisterdeUsersCount();
        ApplicationUser GetUser(string Id);
        ApplicationUser GetUserAllData(string Id);
        void UpdateUser(ApplicationUser user);
    }
}
