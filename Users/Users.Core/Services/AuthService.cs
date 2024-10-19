using Users.Core.Interfaces;

namespace Users.Core.Services
{
    public class AuthService (
        IJwtProvider jwtProvider, 
        IUserRepository userRepository
        ) : IAuthService
    {
        public string GenerateToken(string userId)
        {
            return jwtProvider.GenerateToken(userId);
        }

        public async Task<bool> HasPermission (string userId, string permission)
        {
            await Task.Delay(1000);
            var userPermissions = new[] { "testPermission", "222" };

            return userPermissions.Contains(permission);
        }
    }
}
