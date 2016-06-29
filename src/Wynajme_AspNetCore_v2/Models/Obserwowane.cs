using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wynajme_AspNetCore_v2.Models
{
    public class Obserwowane
    {
        public int Id { get; set; }
        public int OgloszenieId { get; set; }
        public Ogloszenie Ogloszenie { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
