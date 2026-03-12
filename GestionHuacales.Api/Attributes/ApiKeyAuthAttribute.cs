using GestionHuacales.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GestionHuacales.Api.Attributes;

public class ApiKeyAuthAttribute : ServiceFilterAttribute
{
    public ApiKeyAuthAttribute() : base(typeof(ApiKeyAuthorizationFilter))
    {
    }
}
