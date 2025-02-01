using System.ComponentModel.DataAnnotations;

namespace StormEkspress.Models.Entities
{
    public class CourierApplication
    {
        [Required(ErrorMessage = "Ad soyad zorunludur.")]
        [Display(Name = "Adı Soyadı")]
        [StringLength(100, ErrorMessage = "Adı soyadı en fazla 100 karakter olabilir.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Ad soyad yalnızca harf ve boşluk içerebilir.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [Phone(ErrorMessage = "Geçersiz telefon numarası.")]
        [Display(Name = "Telefon Numarası")]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Geçersiz telefon numarası formatı.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "E-posta zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçersiz e-posta adresi.")]
        [Display(Name = "E-posta")]
        public string Email { get; set; }

        [Display(Name = "Motorsiklet Marka/Model")]
        [StringLength(100, ErrorMessage = "Marka/Model en fazla 100 karakter olabilir.")]
        public string Brand { get; set; }

        [Display(Name = "Yaş")]
        [Range(18, 60, ErrorMessage = "Yaş 18 ile 60 arasında olmalıdır.")]
        public int? Age { get; set; }

        [Display(Name = "Özgeçmiş")]
        [StringLength(500, ErrorMessage = "Özgeçmiş en fazla 500 karakter olabilir.")]
        public string Resume { get; set; }

        [Required(ErrorMessage = "Adres zorunludur.")]
        [StringLength(200, ErrorMessage = "Adres en fazla 200 karakter olabilir.")]
        [Display(Name = "Adres")]
        public string Adress { get; set; }

        [Display(Name = "Eğitim Durumu")]
        [StringLength(100, ErrorMessage = "Eğitim durumu en fazla 100 karakter olabilir.")]
        public string Education { get; set; }

        [Display(Name = "Sabika Kaydı")]
        [StringLength(5, ErrorMessage = "Sabika kaydı en fazla 5 karakter olabilir.")]
        public string RapSheet { get; set; }

    }
}
