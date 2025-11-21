using Goodreads.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Goodreads.Infrastructure.Services.EmailService
{
    public class MailKitEmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<MailKitEmailService> _logger;

        public MailKitEmailService(
            IOptions<EmailSettings> emailSettings,
            ILogger<MailKitEmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string to, string subject, string htmlContent)
        {
            try
            {
                _logger.LogInformation("🔵 MAILKIT: Attempting to send email to {Email} with subject '{Subject}'", to, subject);

                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromEmail));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;

                var builder = new BodyBuilder { HtmlBody = htmlContent };
                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();

                // For testing with local SMTP or services like Papercut
                await smtp.ConnectAsync("localhost", 25, SecureSocketOptions.None);

                _logger.LogInformation("✅ MAILKIT: Email sent successfully to {Email}", to);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ MAILKIT: Failed to send email to {Email}", to);
                throw;
            }
        }
    }
}
