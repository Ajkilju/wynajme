using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wynajme_AspNetCore_v2.Models.HomeViewModels
{
    public class NewHomeViewModel
    {
        public IQueryable<Ogloszenie> Ogloszenia { get; set; }
        public IQueryable<Kategoria> Kategorie { get; set; }
        public IQueryable<Miasto> Miasta { get; set; }
    }
}
