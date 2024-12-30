using Microsoft.Extensions.Logging;
using product.common.Settings;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text;

namespace product.application.Email;
public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly EmailSettings _emailSettings;
    private readonly HttpClient _httpClient;

    public EmailService(ILogger<EmailService> logger, IOptions<EmailSettings> emailSettings, HttpClient httpClient)
    {
        _logger = logger;
        _emailSettings = emailSettings.Value;
        _httpClient = httpClient;
    }

    public async Task<bool> SendEmailAsync(string to, string subject, string body)
    {
        try
        {
            _logger.LogInformation($"Enviando correo a {to} con el asunto '{subject}' utilizando SendGrid.");

            var requestContent = new
            {
                personalizations = new[]
                {
                    new
                    {
                        to = new[] { new { email = to } }
                    }
                },
                from = new { email = "rdipaolaj@outlook.com" },
                subject,
                content = new[]
                {
                    new { type = "text/plain", value = body }
                }
            };

            var requestJson = JsonSerializer.Serialize(requestContent);
            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.sendgrid.com/v3/mail/send"),
                Headers =
                {
                    { "Authorization", $"Bearer {_emailSettings.EmailKey}" },
                    { "Accept", "application/json" }
                },
                Content = new StringContent(requestJson, Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Correo enviado exitosamente.");
                return true;
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Falló el envío del correo. Respuesta: {errorResponse}");
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error en {nameof(SendEmailAsync)}: {ex.Message}");
            return false;
        }
    }
}