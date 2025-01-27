using System.ComponentModel.DataAnnotations;

namespace StormEkspress.Models.Entities
{
    public class RestaurantApplication
    {
        [Required(ErrorMessage = "İşletme adı zorunludur.")]
        [Display(Name = "İşletme Adı")]
        public string RestaurantName { get; set; }

        [Required(ErrorMessage = "Adres zorunludur.")]
        [Display(Name = "Adres")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [Phone(ErrorMessage = "Geçersiz telefon numarası.")]
        [Display(Name = "Telefon Numarası")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Geçersiz e-posta formatı.")]
        [Display(Name = "E-posta")]
        public string Email { get; set; } // Opsiyonel

        [Display(Name = "Çalışma Saatleri")]
        public string WorkingHours { get; set; } // Opsiyonel
    }
}
