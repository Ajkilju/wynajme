using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wynajme_AspNetCore_v2.Models.AdminViewModels
{
    public class UsersViewModel
    {
        public IQueryable<ApplicationUser> Users { get; set; }
    }
}
