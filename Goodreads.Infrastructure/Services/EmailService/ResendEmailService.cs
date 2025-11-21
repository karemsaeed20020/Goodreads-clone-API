using Goodreads.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Resend;
using System.Text;
using System.Text.Json;

namespace Goodreads.Infrastructure.Services.EmailService
{
    public class ResendApiEmailService : IEmailService
    {
        private readonly string _apiKey;
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<ResendApiEmailService> _logger;

        public ResendApiEmailService(
            IConfiguration configuration,
            IOptions<EmailSettings> emailSettings,
            ILogger<ResendApiEmailService> logger)
        {
            _apiKey = configuration["Resend:ApiKey"] ?? throw new InvalidOperationException("Resend API key not configured");
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string to, string subject, string htmlContent)
        {
            // Create a new HttpClient for each request (disposed after use)
            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api.resend.com/");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

            try
            {
                // For now, always redirect to your verified email to avoid domain issues
                string originalTo = to;
                if (!to.Contains("karemsaeed321@gmail.com"))
                {
                    to = "karemsaeed321@gmail.com";
                    subject = $"[REDIRECTED from {originalTo}] {subject}";
                    htmlContent = $@"
                        <div style='background-color: #fff3cd; border: 1px solid #ffeaa7; padding: 10px; margin-bottom: 15px;'>
                            <strong>🚨 EMAIL REDIRECTED</strong><br/>
                            <strong>Original recipient:</strong> {originalTo}<br/>
                            <strong>Redirected to:</strong> {to}
                        </div>
                        {htmlContent}";
                }

                _logger.LogInformation("🔵 RESEND: Attempting to send email to {Email}", to);

                var emailRequest = new
                {
                    from = $"{_emailSettings.FromName} <{_emailSettings.FromEmail}>",
                    to = new[] { to },
                    subject = subject,
                    html = htmlContent
                };

                var json = JsonSerializer.Serialize(emailRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("emails", content);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("✅ RESEND: Email sent successfully to {Email}", to);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("❌ RESEND: Failed to send email to {Email}. Status: {Status}, Error: {Error}",
                        to, response.StatusCode, errorContent);
                    throw new Exception($"Resend API error: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ RESEND: Failed to send email to {Email}", to);
                throw;
            }
        }
    }
}