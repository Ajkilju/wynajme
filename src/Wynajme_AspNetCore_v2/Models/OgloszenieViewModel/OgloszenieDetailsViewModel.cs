using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Wynajme_AspNetCore_v2.Models.OgloszenieViewModel
{
    public class SimmlarOgloszenieViewModel
    {
        public int OgloszenieId { get; set; }
        public string Tytul { get; set; }
        public int? Cena { get; set; }
        public string MiniatureString { get; set; }
        public Kategoria Kategoria { get; set; }
        public Miasto Miasto { get; set; }
    }

    public class OgloszenieDetailsViewModel
    {
        public int OgloszenieId { get; set; }
        [Display(Name = "Tytuł")]
        public string Tytul { get; set; }
        [Display(Name = "Treść")]
        public string Tresc { get; set; }
        [Display(Name = "Dodano")]
        [DataType(DataType.Date)]
        public DateTime DataDodania { get; set; }
        public int? Cena { get; set; }
        public List<string> ImageString { get; set; }
        public Kategoria Kategoria { get; set; }
        public Miasto Miasto { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string UserImageString { get; set; }
        public int Powierzchnia { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Telefon { get; set; }
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public Boolean Zmywarka { get; set; }
        [Display(Name = "Lodowka")]
        public Boolean Lodowka { get; set; }
        [Display(Name = "Kuchenka mikrofalowa")]
        public Boolean Mikrofala { get; set; }
        [Display(Name = "Kuchnia gazowa")]
        public Boolean KuchniaGaz { get; set; }
        [Display(Name = "Kuchnia elektryczna")]
        public Boolean KuchniaEl { get; set; }
        public Boolean Pralka { get; set; }
        public Boolean Wanna { get; set; }
        public Boolean Prysznic { get; set; }
        [Display(Name = "Dodatkowe wyposażenie")]
        public string DodatkoweWyposazenie { get; set; }
        public IEnumerable<SimmlarOgloszenieViewModel> PodobneOgloszenia { get; set; }

        public OgloszenieDetailsViewModel(Ogloszenie ogloszenie)
        {
            OgloszenieId = ogloszenie.OgloszenieId;
            Tytul = ogloszenie.Tytul;
            Tresc = ogloszenie.Tresc;
            DataDodania = ogloszenie.DataDodania;
            Cena = ogloszenie.Cena;
            Kategoria = ogloszenie.Kategoria;
            Miasto = ogloszenie.Miasto;
            Powierzchnia = ogloszenie.Powierzchnia;
            Telefon = ogloszenie.Telefon;
            Email = ogloszenie.Email;
            Zmywarka = ogloszenie.Zmywarka;
            Lodowka = ogloszenie.Lodowka;
            Mikrofala = ogloszenie.Mikrofala;
            KuchniaEl = ogloszenie.KuchniaEl;
            KuchniaGaz = ogloszenie.KuchniaGaz;
            Pralka = ogloszenie.Pralka;
            Wanna = ogloszenie.Wanna;
            Prysznic = ogloszenie.Prysznic;
            DodatkoweWyposazenie = ogloszenie.DodatkoweWyposazenie;

            if (ogloszenie.UserId == null)
            {
                UserId = null;
                UserImageString = "/images/DefaultUserPhoto.png";
                UserName = "Unknown";
                UserLastName = "Unknown";
            }
            else
            {
                UserId = ogloszenie.UserId;
                UserImageString = GetImageString(ogloszenie.User.Image);
                UserName = ogloszenie.User.Name;
                UserLastName = ogloszenie.User.LastName;
            }

            ImageString = new List<string>();
            foreach (var img in ogloszenie.Image)
            {
                ImageString.Add(GetImageString(img.ImageContent));
            }
            if (ImageString.Count == 0)
            {
                ImageString.Add("/images/DefaultOglPhoto.png");
            }
        }

        public void SetSimmlarOgloszenia(IQueryable<Ogloszenie> ogls)
        {
            var list = new List<SimmlarOgloszenieViewModel>();

            foreach(var ogl in ogls)
            {
                list.Add(new SimmlarOgloszenieViewModel
                {
                    OgloszenieId = ogl.OgloszenieId,
                    Tytul = ogl.Tytul,
                    Cena = ogl.Cena,
                    Kategoria = ogl.Kategoria,
                    Miasto = ogl.Miasto,
                    MiniatureString = ogl.Miniature == null ? "/images/DefaultOglPhoto.png" : GetImageString(ogl.Miniature) 
                });
            }
            PodobneOgloszenia = list;
        }

        //przeniesc metode do innego pliku, aby udostępnic ją innym klasom
        string GetImageString(byte[] img)
        {
            var base64 = Convert.ToBase64String(img);
            return String.Format("data:image/gif;base64,{0}", base64);
        }
    }
}
