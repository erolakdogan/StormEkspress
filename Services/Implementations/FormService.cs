using StormEkspress.Helper;
using StormEkspress.Models.Entities;
using StormEkspress.Services.Interfaces;
using System.Web;

public class FormService : IFormService
{
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;

    public FormService(IEmailService emailService, IConfiguration configuration)
    {
        _emailService = emailService;
        _configuration = configuration;
    }
    // Kurye başvuru formu - SubmitApplicationAsync metodunu oluşturduk
    public async Task SubmitApplicationAsync(object application, string formType)
    {
        string sanitizedName, sanitizedEmail, sanitizedPhone, sanitizedAddress, sanitizedResume;

        if (formType == "Courier")
        {
            var courierApp = application as CourierApplication;

            // Kurye başvurusu ise
            sanitizedName = SanitizeInput(courierApp?.Name);
            sanitizedEmail = SanitizeInput(courierApp?.Email);
            sanitizedPhone = SanitizeInput(courierApp?.Phone);
            sanitizedAddress = SanitizeInput(courierApp?.Adress);
            sanitizedResume = SanitizeInput(courierApp?.Resume);

            // Doğrulama işlemi
            if (string.IsNullOrEmpty(sanitizedName))
                throw new ValidationException("Ad alanı zorunludur.");
            if (string.IsNullOrEmpty(sanitizedPhone))
                throw new ValidationException("Telefon numarası zorunludur.");
            if (string.IsNullOrEmpty(sanitizedEmail))
                throw new ValidationException("E-posta zorunludur.");
            if (string.IsNullOrEmpty(sanitizedAddress))
                throw new ValidationException("Adres zorunludur.");

            var toEmail = _configuration["EmailSettings:ToEmail"];
            var subject = "Yeni Kurye Başvurusu";
            var body = BuildEmailContent(application, "Courier");

            try
            {
                await _emailService.SendEmailAsync(toEmail, subject, body);
            }
            catch (Exception ex)
            {
                throw new EmailSendException($"E-posta gönderilemedi: {ex.Message}");
            }
        }
        else if (formType == "Restaurant")
        {
            // Restoran başvurusu ise
            var restaurantApp = application as RestaurantApplication;
            sanitizedName = SanitizeInput(restaurantApp?.RestaurantName);
            sanitizedEmail = SanitizeInput(restaurantApp?.Email);
            sanitizedPhone = SanitizeInput(restaurantApp?.Phone);
            sanitizedAddress = SanitizeInput(restaurantApp?.Adress);
            var sanitizedWorkingHours = SanitizeInput(restaurantApp?.WorkingHours);

            // Doğrulama işlemi
            if (string.IsNullOrEmpty(sanitizedName))
                throw new ValidationException("Restoran adı zorunludur.");
            if (string.IsNullOrEmpty(sanitizedPhone))
                throw new ValidationException("Telefon numarası zorunludur.");
            if (string.IsNullOrEmpty(sanitizedEmail))
                throw new ValidationException("E-posta zorunludur.");
            if (string.IsNullOrEmpty(sanitizedAddress))
                throw new ValidationException("Adres zorunludur.");

            var toEmail = _configuration["EmailSettings:ToEmail"];
            var subject = "Yeni Restoran Başvurusu";
            var body = BuildEmailContent(application, "Restaurant");

            try
            {
                await _emailService.SendEmailAsync(toEmail, subject, body);
            }
            catch (Exception ex)
            {
                throw new EmailSendException($"E-posta gönderilemedi: {ex.Message}");
            }
        }
        else
        {
            throw new ValidationException("Geçersiz form türü.");
        }
    }

    // XSS önlemi - Kullanıcıdan gelen verileri sanitize etme
    private string SanitizeInput(string input)
    {
        return HttpUtility.HtmlEncode(input); // HTML encode
    }

    // E-posta içeriğini oluşturma - Courier veya Restaurant başvurusu için içerik
    private string BuildEmailContent(object application, string formType)
    {
        string body = string.Empty;

        if (formType == "Courier")
        {
            var courierApp = application as CourierApplication;

            string sanitizedName = SanitizeInput(courierApp?.Name);
            string sanitizedEmail = SanitizeInput(courierApp?.Email);
            string sanitizedPhone = SanitizeInput(courierApp?.Phone);
            string sanitizedAddress = SanitizeInput(courierApp?.Adress);
            string sanitizedResume = SanitizeInput(courierApp?.Resume);
            string sanitizedRapSheet = SanitizeInput(courierApp?.RapSheet);

            body = $"<h1>Yeni Kurye Başvurusu</h1>" +
                   $"<p><strong>Ad Soyad:</strong> {sanitizedName}</p>" +
                   $"<p><strong>E-posta:</strong> {sanitizedEmail}</p>" +
                   $"<p><strong>Telefon:</strong> {sanitizedPhone}</p>" +
                   $"<p><strong>Adres:</strong> {sanitizedAddress}</p>" +
                   $"<p><strong>Özgeçmiş:</strong> {sanitizedResume}</p>" +
                   $"<p><strong>Sabika Kaydı:</strong> {sanitizedRapSheet}</p>";
        }
        else if (formType == "Restaurant")
        {
            var restaurantApp = application as RestaurantApplication;

            string sanitizedRestaurantName = SanitizeInput(restaurantApp?.RestaurantName);
            string sanitizedEmail = SanitizeInput(restaurantApp?.Email);
            string sanitizedPhone = SanitizeInput(restaurantApp?.Phone);
            string sanitizedAddress = SanitizeInput(restaurantApp?.Adress);
            string sanitizedWorkingHours = SanitizeInput(restaurantApp?.WorkingHours);

            body = $"<h1>Yeni Restoran Başvurusu</h1>" +
                   $"<p><strong>Restoran Adı:</strong> {sanitizedRestaurantName}</p>" +
                   $"<p><strong>E-posta:</strong> {sanitizedEmail}</p>" +
                   $"<p><strong>Telefon:</strong> {sanitizedPhone}</p>" +
                   $"<p><strong>Adres:</strong> {sanitizedAddress}</p>" +
                   $"<p><strong>Çalışma Saatleri:</strong> {sanitizedWorkingHours}</p>";
        }

        return body;
    }

}