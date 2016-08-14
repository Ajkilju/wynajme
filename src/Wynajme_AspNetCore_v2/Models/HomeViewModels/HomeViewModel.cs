using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wynajme_AspNetCore_v2.Models.HomeViewModels
{
    public class NewHomeViewModel
    {
        public List<Ogloszenie> Ogloszenia { get; set; }
        public List<Kategoria> Kategorie { get; set; }
        public List<Miasto> Miasta { get; set; }
    }
}
