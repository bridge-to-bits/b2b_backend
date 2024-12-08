using System.Security.Claims;

namespace Core.Interfaces.Services;

public interface IAuthService
{
    string GenerateToken(string userId);
    Task<bool> HasPermission(string userId, string permission);
    public Task CreateUserRole(string userId, string roleName);
    public Task SetPermissions(string userId, IEnumerable<string> permissions);
    public ClaimsPrincipal? ValidateToken(string token);
}
