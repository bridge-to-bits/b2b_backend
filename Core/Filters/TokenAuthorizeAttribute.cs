using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Filters;

[AttributeUsage(AttributeTargets.Method)]
public class TokenAuthorizeAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var authService = context.HttpContext.RequestServices.GetRequiredService<IAuthService>();

        var token = ExtractToken(context.HttpContext);
        if (string.IsNullOrEmpty(token))
        {
            context.Result = new UnauthorizedObjectResult("Missing token.");
            return;
        }

        try
        {
            var claimsPrincipal = authService.ValidateToken(token);
            if (claimsPrincipal == null)
            {
                context.Result = new UnauthorizedObjectResult("Invalid token.");
                return;
            }

            context.HttpContext.User = claimsPrincipal;
        }
        catch (Exception ex)
        {
            context.Result = new UnauthorizedObjectResult($"Unauthorized: {ex.Message}");
            return;
        }

        await next();
    }

    private string? ExtractToken(HttpContext context)
    {
        var token = context.Request.Cookies["access_token"];

        if (string.IsNullOrEmpty(token))
        {
            var authHeader = context.Request.Headers.Authorization.FirstOrDefault();
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = authHeader["Bearer ".Length..].Trim();
            }
        }

        return token;
    }
}
