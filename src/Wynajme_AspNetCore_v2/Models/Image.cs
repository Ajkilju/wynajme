using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wynajme_AspNetCore_v2.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public int OgloszenieId { get; set; }
        public Ogloszenie Ogloszenie { get; set; }
        public byte[] ImageContent { get; set; }
        public string ImageMimeType { get; set; }
    }
}
