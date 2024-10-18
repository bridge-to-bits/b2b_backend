namespace Users.Core.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(string userId);
        Task<bool> HasPermission(string userId, string permission);
    }
}
