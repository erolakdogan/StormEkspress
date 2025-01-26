using StormEkspress.Helper;
using StormEkspress.Models.Entities;
using StormEkspress.Services.Interfaces;

namespace StormEkspress.Services.Implementations
{
    public class FormService : IFormService
    {
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public FormService(IEmailService emailService, IConfiguration configuration)
        {
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task SubmitCourierApplication(CourierApplication application)
        {
            if (string.IsNullOrEmpty(application.Name))
                throw new ValidationException("Ad alanı zorunludur.");

            var toEmail = _configuration["EmailSettings:ToEmail"];
            var subject = "Yeni Kurye Başvurusu";
            var body = $"<h1>Yeni Kurye Başvurusu</h1>" +
                       $"<p><strong>Ad:</strong> {application.Name}</p>" +
                       $"<p><strong>E-posta:</strong> {application.Email}</p>" +
                       $"<p><strong>Telefon:</strong> {application.Phone}</p>";

            try
            {
                await _emailService.SendEmailAsync(toEmail, subject, body);
            }
            catch (Exception ex)
            {
                throw new EmailSendException($"E-posta gönderilemedi: {ex.Message}");
            }
        }

        public async Task SubmitRestaurantApplication(RestaurantApplication application)
        {
            if (string.IsNullOrEmpty(application.RestaurantName))
                throw new ValidationException("Restoran Adı zorunludur.");

            var toEmail = _configuration["EmailSettings:ToEmail"];
            var subject = "Yeni Restoran Başvurusu";
            var body = $"<h1>Yeni Restoran Başvurusu</h1>" +
                       $"<p><strong>Restoran Adı:</strong> {application.RestaurantName}</p>" +
                       $"<p><strong>E-posta:</strong> {application.Email}</p>" +
                       $"<p><strong>Telefon:</strong> {application.Phone}</p>";

            try
            {
                await _emailService.SendEmailAsync(toEmail, subject, body);
            }
            catch (Exception ex)
            {
                throw new EmailSendException($"E-posta gönderilemedi: {ex.Message}");
            }
        }
    }
}
