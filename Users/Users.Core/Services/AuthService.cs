using Users.Core.Interfaces;

namespace Users.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IUserRepository _userRepository;

        public AuthService(IJwtProvider jwtProvider, IUserRepository userRepository)
        {
            _jwtProvider = jwtProvider;
            _userRepository = userRepository;
        }

        public string GenerateToken(string userId)
        {
            return _jwtProvider.GenerateToken(userId);
        }

        public async Task<bool> HasPermission (string userId, string permission)
        {
            await Task.Delay(1000);
            var userPermissions = new[] { "testPermission", "222" };

            return userPermissions.Contains(permission);
        }
    }
}
