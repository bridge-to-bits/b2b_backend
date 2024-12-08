using System.Security.Claims;

namespace Core.Interfaces.Auth;

public interface IJwtProvider
{
    string GenerateToken(string userId);
    public ClaimsPrincipal? ValidateToken(string token);
}
