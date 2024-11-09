namespace Users.Core.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(string userId);
}
