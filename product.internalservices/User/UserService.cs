using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using product.common.Responses;
using product.common.Settings;
using product.common.Validations;
using product.internalservices.Base;
using System.Net.Http.Json;
using System.Text.Json;

namespace product.internalservices.User;
internal class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IBaseService _baseService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ApiSettings _settings;

    public UserService(
        ILogger<UserService> logger,
        IBaseService baseService,
        IHttpClientFactory httpClientFactory,
        IOptions<ApiSettings> settings)
    {
        _logger = logger;
        _baseService = baseService;
        _httpClientFactory = httpClientFactory;
        _settings = settings.Value;
    }
    public async Task<ApiResponse<IEnumerable<string>>> GetAdminEmailsAsync(CancellationToken cancellationToken)
    {
        using HttpClient httpClient = _httpClientFactory.CreateClient("CustomClient");
        string path = GetAdminEmailsPath();
        httpClient.BaseAddress = new Uri(_settings.UrlMsUser);
        HttpResponseMessage httpResponse;
        try
        {
            httpResponse = await _baseService.GetAsync(httpClient, path);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(GetAdminEmailsAsync)}: {ex.Message}");
            return ApiResponseHelper.CreateErrorResponse<IEnumerable<string>>("Error al obtener los correos de los administradores", 500);
        }
        if (!CommonHttpValidation.ValidHttpResponse(httpResponse))
        {
            var errorContent = await httpResponse.Content.ReadAsStringAsync();
            _logger.LogError($"Error in {nameof(GetAdminEmailsAsync)}: {errorContent}");
            return ApiResponseHelper.CreateErrorResponse<IEnumerable<string>>("Error al obtener los correos de los administradores", (int)httpResponse.StatusCode);
        }
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var response = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<string>>>(options);
        return response ?? ApiResponseHelper.CreateErrorResponse<IEnumerable<string>>("Error al obtener los correos de los administradores", 500);
    }

    #region Private methods

    private static string GetAdminEmailsPath()
    {
        return $"touch/user/api/v1/User/admin-emails";
    }

    #endregion
}
