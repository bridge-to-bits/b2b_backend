using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Users.Core.Interfaces;

namespace Users.Core.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizePermissionAttribute(string permission) : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authService = context.HttpContext.RequestServices.GetService<IAuthService>();

            var userId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (userId == null)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                return;
            }

            var hasPermission = await authService.HasPermission(userId, permission);
            if (!hasPermission)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
            }
        }
    }
}
