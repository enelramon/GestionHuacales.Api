using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GestionHuacales.Api.Filters;

public class ApiKeyAuthorizationFilter : IAuthorizationFilter
{
    private const string API_KEY_HEADER = "X-API-KEY";
    private readonly IConfiguration _configuration;

    public ApiKeyAuthorizationFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Obtener la API Key del header
        if (!context.HttpContext.Request.Headers.TryGetValue(API_KEY_HEADER, out var extractedApiKey))
        {
            context.Result = new UnauthorizedObjectResult(new { message = "API Key no proporcionada" });
            return;
        }

        // Obtener la API Key válida desde la configuración
        var apiKey = _configuration.GetValue<string>("ApiKey");

        // Validar la API Key
        if (string.IsNullOrWhiteSpace(apiKey) || !apiKey.Equals(extractedApiKey))
        {
            context.Result = new UnauthorizedObjectResult(new { message = "API Key inválida" });
            return;
        }
    }
}
