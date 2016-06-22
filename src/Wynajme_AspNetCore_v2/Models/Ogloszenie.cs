using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Wynajme_AspNetCore_v2.Models
{
    


    public class Ogloszenie
    {
        //w tabeli "ogloszenie" przechowywany jest klucz obcy 
        //kategorii, miasta i uzytkownika. 
        //Kategoria, miasto i uzytkownik zawietraja liste ogloszen, natomiast
        //ogloszenie jest przypisane tylko do jednej kategowii, miasta i uzytkownika.
        //Odwrotna sytuacja zachodzi w przypadku tabewli "image". Jedno ogloszenie
        //moze zawierac kilka zdjęc, natomiast jedno zdjęcie przypisane jest tylko do jednego ogłoszenia,
        //dlatego klucz obcy znajduje się w tabeli "image"

        public int OgloszenieId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Tytuł")]
        public string Tytul { get; set; }

        [Required]
        [Display(Name = "Treść")]
        [StringLength(2000)]
        public string Tresc { get; set; }

        [Required]
        [Display(Name = "Dodano")]
        [DataType(DataType.Date)]
        public DateTime DataDodania { get; set; }

        public int? Cena { get; set; }

        public byte[] Miniature { get; set; }
        public string MiniatureMimeType { get; set; }

        public List<Image> Image { get; set; }
        //[ForeignKey("MiniatureId")]

        [Display(Name = "Kategoria")]
        public int KategoriaId { get; set; }

        [ForeignKey("KategoriaId")]
        public Kategoria Kategoria { get; set; }

        [Display(Name = "Miasto")]
        public int MiastoId { get; set; }

        [ForeignKey("MiastoId")]
        public Miasto Miasto { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    
        public int Powierzchnia { get; set; }

        //kontakt
        [DataType(DataType.PhoneNumber)]
        public string Telefon { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //wyposarzenie
        //kuchnia
        public Boolean Zmywarka { get; set; }
        [Display(Name = "Lodowka")]
        public Boolean Lodowka { get; set; }
        [Display(Name = "Kuchenka mikrofalowa")]
        public Boolean Mikrofala { get; set; }
        [Display(Name = "Kuchnia gazowa")]
        public Boolean KuchniaGaz { get; set; }
        [Display(Name = "Kuchnia elektryczna")]
        public Boolean KuchniaEl { get; set; }
        
        //lazeinka
        public Boolean Pralka { get; set; }
        public Boolean Wanna { get; set; }
        public Boolean Prysznic { get; set; }

        [Display(Name = "Dodatkowe wyposażenie")]
        [StringLength(100)]
        public string DodatkoweWyposazenie { get; set; }
        
    }
}
