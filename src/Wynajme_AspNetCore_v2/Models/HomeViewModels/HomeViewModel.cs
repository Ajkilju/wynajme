using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wynajme_AspNetCore_v2.Models.HomeViewModels
{
    public class NewHomeViewModel
    {
        public IEnumerable<Ogloszenie> Ogloszenia { get; set; }
        public IEnumerable<Kategoria> Kategorie { get; set; }
        public IEnumerable<Miasto> Miasta { get; set; }
    }
}
