using System.ComponentModel.DataAnnotations;

namespace StormEkspress.Models.Entities
{
    public class RestaurantApplication
    {
        [Required(ErrorMessage = "İşletme adı zorunludur.")]
        [Display(Name = "İşletme Adı")]
        [StringLength(100, ErrorMessage = "İşletme adı en fazla 100 karakter olabilir.")]
        public string RestaurantName { get; set; }

        [Required(ErrorMessage = "Adres zorunludur.")]
        [StringLength(200, ErrorMessage = "Adres en fazla 200 karakter olabilir.")]
        [Display(Name = "Adres")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [Phone(ErrorMessage = "Geçersiz telefon numarası.")]
        [Display(Name = "Telefon Numarası")]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Geçersiz telefon numarası formatı.")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Geçersiz e-posta formatı.")]
        [Display(Name = "E-posta")]
        public string Email { get; set; }

        [Display(Name = "Çalışma Saatleri")]
        [StringLength(100, ErrorMessage = "Çalışma saatleri en fazla 100 karakter olabilir.")]
        public string WorkingHours { get; set; }
    }
}
