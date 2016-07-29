using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wynajme_AspNetCore_v2.Models.OgloszenieViewModel
{
    public class OgloszenieCreateViewModel
    {
        public Ogloszenie Ogloszenie { get; set; }

        public List<byte[]> LoadedImages { get; set; }

        public bool IsUserLogedIn { get; set; }
        public string LogedUserEmail { get; set; }
        public string LogedUserPhone { get; set; }

        public bool IsImagesLoaded { get; set; }

    }
}
