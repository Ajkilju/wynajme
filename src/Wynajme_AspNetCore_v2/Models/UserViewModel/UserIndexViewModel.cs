using Sakura.AspNetCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wynajme_AspNetCore_v2.Models.UserViewModel
{
    public class UserIndexViewModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public IPagedList<Ogloszenie> Ogloszenia { get; set; }
        public byte[] UserImage { get; set; }
        [DataType(DataType.Date)]
        public DateTime RegisterDate { get; set; }
        public DateTime LastLogIn { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public UserIndexViewModel (ApplicationUser user, int page = 1)
        {
            Name = user.Name;
            LastName = user.LastName;
            Ogloszenia = user.Ogloszenia.ToPagedList(10, page);
            UserImage = user.Image;
            RegisterDate = user.RegisterDate;
            UserId = user.Id;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
        }
    }
}
