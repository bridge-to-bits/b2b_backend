using System.Security.Claims;

namespace Users.Core.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(string userId);
    public ClaimsPrincipal? ValidateToken(string token);
}
