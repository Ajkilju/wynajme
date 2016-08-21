using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wynajme_AspNetCore_v2.Models.OgloszenieViewModel
{
    public class OgloszenieCreateViewModel
    {
        public Ogloszenie Ogloszenie { get; set; }
        public ApplicationUser LoggedUser { get; set; }
        public SelectList KategorieId { get; set; }
        public SelectList MiastaId { get; set; }
    }
}
