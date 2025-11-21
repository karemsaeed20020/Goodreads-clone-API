using Goodreads.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Goodreads.Infrastructure.Services.EmailService
{
    internal class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly EmailSettings _emailSettings;

        public EmailService(ILogger<EmailService> logger, IOptions<EmailSettings> emailSettings)
        {
            _logger = logger;
            _emailSettings = emailSettings.Value; // Get the actual settings from IOptions
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            try
            {
                if (_emailSettings.UseSmtp4Dev)
                {
                    // For development with smtp4dev
                    using var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port);
                    client.EnableSsl = false;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_emailSettings.FromEmail, _emailSettings.FromName),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };
                    mailMessage.To.Add(email);

                    await client.SendMailAsync(mailMessage);
                    _logger.LogInformation("Email sent successfully to {Email}", email);
                }
                else
                {
                    // For production with real SMTP
                    using var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
                    {
                        EnableSsl = true,
                        Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password)
                    };

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_emailSettings.FromEmail, _emailSettings.FromName),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };
                    mailMessage.To.Add(email);

                    await client.SendMailAsync(mailMessage);
                    _logger.LogInformation("Email sent successfully to {Email}", email);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {Email}", email);
                throw;
            }
        }
    }
}