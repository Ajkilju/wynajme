using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Wynajme_AspNetCore_v2.Models.OgloszenieViewModel
{
    public class OgloszenieDetailsViewModel
    {
        public Ogloszenie Ogloszenie { get; set; }
        public List<Ogloszenie> PodobneOgloszenia { get; set; }
        public string LogInUserId { get; set; }
        public bool Obserwowane { get; set; }
    }
}
