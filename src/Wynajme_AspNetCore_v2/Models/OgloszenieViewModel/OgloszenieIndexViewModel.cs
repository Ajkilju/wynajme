using Sakura.AspNetCore.Mvc;
using Sakura.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wynajme_AspNetCore_v2.Models.OgloszenieViewModel
{
    public class OgloszenieIndexViewModel
    {
        public IPagedList<Ogloszenie> Ogloszenia { get; set; }
        public List<Kategoria> Kategorie { get; set; }
        public List<Miasto> Miasta { get; set; }
        public string AktualnaKategoria { get; set; }
        public string AktualneMiasto { get; set; }
        public string SearchString { get; set; }
        public string SearchStringRoute { get; set; }
        public int? CenaOd { get; set; }
        public List<int?> CenaOdList { get; set; }
        public List<int?> CenaDoList { get; set; }
        public int? CenaDo { get; set; }
        public int? SortOrderRoute { get; set; }
        public List<string> SortOrderList { get; set; }
        public string SortOrder { get; set; }
        public List<int> PageSizeList { get; set; }
        public int PageSize { get; set; }
        public ApplicationUser User { get; set; }

        public Boolean? Zmywarka { get; set; }
        public Boolean? Lodowka { get; set; }
        public Boolean? Mikrofala { get; set; }
        public Boolean? KuchniaGaz { get; set; }
        public Boolean? KuchniaEl { get; set; }
        public Boolean? Pralka { get; set; }
        public Boolean? Wanna { get; set; }
        public Boolean? Prysznic { get; set; }
    }
}
