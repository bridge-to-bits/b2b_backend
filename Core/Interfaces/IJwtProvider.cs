using System.Security.Claims;

namespace Core.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(string userId);
    public ClaimsPrincipal? ValidateToken(string token);
}
