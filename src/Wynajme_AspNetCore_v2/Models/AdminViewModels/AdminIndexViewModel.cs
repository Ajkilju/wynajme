using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wynajme_AspNetCore_v2.Models.AdminViewModels
{
    public class AdminIndexViewModel
    {
        public int UsersCount { get; set; }
        public int AdminsCount { get; set; }
        public int OgloszeniaCount { get; set; }
        public int KategorieCount { get; set; }
        public int LokalizacjeCount { get; set; }
    }
}
