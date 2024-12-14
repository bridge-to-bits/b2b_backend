using Core.Interfaces.Auth;

namespace Core.Services;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    public bool VerifyHashedPassword(string providedPassword, string hashedPassword) =>
        BCrypt.Net.BCrypt.EnhancedVerify(providedPassword, hashedPassword);
}
