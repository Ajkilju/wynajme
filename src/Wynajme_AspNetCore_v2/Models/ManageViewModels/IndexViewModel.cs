using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Sakura.AspNetCore;

namespace Wynajme_AspNetCore_v2.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public byte[] Image { get; set; }
        public IPagedList<Ogloszenie> Ogloszenia { get; set; }
        public IPagedList<Obserwowane> Obserwowane { get; set; }
    }
}
