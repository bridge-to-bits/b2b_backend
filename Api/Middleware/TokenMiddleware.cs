namespace Api.Middleware;

using Core.Interfaces.Services;

public class TokenMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var token = ExtractToken(context);

        if (!string.IsNullOrEmpty(token))
        {
            using var scope = serviceScopeFactory.CreateScope();
            var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();

            try
            {
                var claimsPrincipal = authService.ValidateToken(token);

                if (claimsPrincipal != null)
                {
                    context.User = claimsPrincipal;
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized: Invalid token.");
                    return;
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync($"Unauthorized: {ex.Message}");
                return;
            }
        }

        await next(context);
    }

    private string ExtractToken(HttpContext context)
    {
        var token = context.Request.Cookies["access_token"];

        if (string.IsNullOrEmpty(token))
        {
            var authHeader = context.Request.Headers.Authorization.FirstOrDefault();
            if (authHeader != null && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = authHeader["Bearer ".Length..].Trim();
            }
        }

        return token;
    }
}
