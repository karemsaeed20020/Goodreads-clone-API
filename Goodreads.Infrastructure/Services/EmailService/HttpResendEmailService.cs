using Goodreads.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Goodreads.Infrastructure.Services.EmailService
{
    public class HttpResendEmailService : IEmailService
    {
        private readonly ILogger<HttpResendEmailService> _logger;
        private readonly EmailSettings _emailSettings;
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public HttpResendEmailService(
            ILogger<HttpResendEmailService> logger,
            IOptions<EmailSettings> emailSettings,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _logger = logger;
            _emailSettings = emailSettings.Value;
            _httpClient = httpClientFactory.CreateClient();
            _apiKey = configuration["Resend:ApiKey"];

            // Configure HttpClient for Resend API
            _httpClient.BaseAddress = new Uri("https://api.resend.com/");
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            try
            {
                var requestData = new
                {
                    from = $"{_emailSettings.FromName} <{_emailSettings.FromEmail}>",
                    to = new[] { email },
                    subject = subject,
                    html = body
                };

                var json = JsonSerializer.Serialize(requestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _logger.LogInformation("📧 Sending email via Resend API to {Email}", email);

                var response = await _httpClient.PostAsync("emails", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation("✅ Email sent successfully to {Email}", email);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("❌ Resend API error: {StatusCode} - {Error}",
                        response.StatusCode, errorContent);
                    throw new Exception($"Resend API error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Failed to send email to {Email}", email);
                throw;
            }
        }
    }
}