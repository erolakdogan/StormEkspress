using System.ComponentModel.DataAnnotations;

namespace StormEkspress.Models.Entities
{
    public class RestaurantApplication
    {
        [Required(ErrorMessage = "Restaurant Adı zorunludur.")]
        public string RestaurantName { get; set; }

        [Required(ErrorMessage = "Email zorunludur")]
        [EmailAddress(ErrorMessage = "Geçersiz e-posta adresi.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon alanı zorunludur.")]
        [Phone(ErrorMessage = "Geçersiz telefon numarası.")]
        public string Phone { get; set; }
    }
}
