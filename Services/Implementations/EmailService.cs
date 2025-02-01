using StormEkspress.Helper;
using StormEkspress.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace StormEkspress.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _fromEmail;
        private readonly string _username;
        private readonly string _password;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _smtpServer = _configuration["EmailSettings:SmtpServer"];
            _smtpPort = int.Parse(_configuration["EmailSettings:Port"]);
            _fromEmail = _configuration["EmailSettings:FromEmail"];
            _username = _configuration["EmailSettings:Username"];
            _password = _configuration["EmailSettings:Password"];
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var emailMessage = CreateEmailMessage(toEmail, subject, body);

                using (var client = new SmtpClient())
                {
                    await ConnectAndAuthenticateAsync(client).ConfigureAwait(false);
                    await SendEmailMessageAsync(client, emailMessage).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                }
            }
            catch (SmtpCommandException smtpEx)
            {
                // Log or handle SMTP specific exceptions (e.g., authentication failure)
                Console.Error.WriteLine($"SMTP Error: {smtpEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Log or handle generic exceptions (e.g., network issues)
                Console.Error.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        private MimeMessage CreateEmailMessage(string toEmail, string subject, string body)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Your Name", _fromEmail));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            return emailMessage;
        }

        private async Task ConnectAndAuthenticateAsync(SmtpClient client)
        {
            await client.ConnectAsync(_smtpServer, _smtpPort, true).ConfigureAwait(false); // TLS bağlantısı
            await client.AuthenticateAsync(_username, _password).ConfigureAwait(false); // SMTP kimlik doğrulaması
        }

        private async Task SendEmailMessageAsync(SmtpClient client, MimeMessage emailMessage)
        {
            await client.SendAsync(emailMessage).ConfigureAwait(false); // E-posta gönderimi
        }
    }
}
