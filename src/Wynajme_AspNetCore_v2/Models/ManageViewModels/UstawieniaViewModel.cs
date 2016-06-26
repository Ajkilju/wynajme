using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wynajme_AspNetCore_v2.Models.ManageViewModels
{
    public class UstawieniaViewModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public byte[] Image { get; set; }
        public string PhoneNumber { get; set; }
    }
}
