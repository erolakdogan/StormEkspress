using System.ComponentModel.DataAnnotations;

namespace StormEkspress.Models.Entities
{
    public class CourierApplication
    {
        [Required(ErrorMessage = "Ad soyad zorunludur.")]
        [Display(Name = "Adı Soyadı")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [Phone(ErrorMessage = "Geçersiz telefon numarası.")]
        [Display(Name = "Telefon Numarası")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "E-posta zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçersiz e-posta adresi.")]
        [Display(Name = "E-posta")]
        public string Email { get; set; }

        [Display(Name = "Motorsiklet Marka/Model")]
        public string Brand { get; set; }

        [Display(Name = "Yaş")]
        public int? Age { get; set; } // Nullable (Opsiyonel)

        [Display(Name = "Özgeçmiş")]
        public string Resume { get; set; }

        [Required(ErrorMessage = "Adres zorunludur.")]
        [Display(Name = "Adres")]
        public string Adress { get; set; }

        [Display(Name = "Eğitim Durumu")]
        public string Education { get; set; }

    }
}
