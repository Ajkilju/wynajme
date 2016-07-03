using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wynajme_AspNetCore_v2.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {      
        public string Name { get; set; }
        public string LastName { get; set; }
        public List<Ogloszenie> Ogloszenia { get; set; }
        public byte[] Image { get; set; }
        public string ImageMimeType { get; set; }

        public DateTime RegisterDate { get; set; }
        public DateTime LastLogIn { get; set; }

        public List<Obserwowane> Obserwowane { get; set; }
    }
 
}
