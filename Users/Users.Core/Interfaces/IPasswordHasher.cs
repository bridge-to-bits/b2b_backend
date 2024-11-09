namespace Users.Core.Interfaces;

public interface IPasswordHasher
{
    public string HashPassword(string password);

    public bool VerifyHashedPassword(string providedPassword, string hashedPassword);
}
