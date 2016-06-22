using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wynajme_AspNetCore_v2.Models
{
    public class Miasto
    {
        [Display(Name = "Miasto")]
        public int MiastoId { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "Miasto")]
        public string Nazwa { get; set; }

        //virtual????
        //public virtual List<Ogloszenie> Ogloszenia { get; set; }
        public List<Ogloszenie> Ogloszenia { get; set; }
    }
}
