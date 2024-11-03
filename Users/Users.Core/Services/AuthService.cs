using Users.Core.Interfaces;
using Users.Core.Models;

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

        public async Task<bool> HasPermission(string userId, string permission)
        {
            var user = await userRepository.GetUserWithRolesAndGrants(user => user.Id.ToString() == userId);

            if (user == null)
            {
                return false;
            }

            return HasPermission(user.Roles, permission);
        }

        public async Task SetPermissions(string userId, IEnumerable<string> permissions)
        {
            var user = await userRepository.GetUserWithRolesAndGrants(user => user.Id.ToString() == userId)
                ?? throw new Exception("user does not exist");

            var userRole = user.Roles.First() 
                ?? throw new Exception("any role is not attached to user");

            var userGrants = userRole.Grants.ToList();


            if (HasPermissions(userGrants, permissions))
            {
                throw new Exception("some permissions already exist");
            }

            var grants = permissions
                .Select(permission => new Grant() { Permission = permission, RoleId = userRole.Id })
                .ToList();

            await userRepository.CreateGrants(grants);
        }

        public async Task CreateUserRole(string userId, string roleName)
        {
            var user = await userRepository.GetUser(user => user.Id.ToString() == userId)
                ?? throw new Exception("user does not exist");

            await userRepository.CreateRole(new Role() { Name = roleName, UserId = Guid.Parse(userId) });
        }

        private static bool HasPermissions(IEnumerable<Grant> userGrants, IEnumerable<string> permissions)
        {
            return userGrants
                .Select(grant => grant.Permission)
                .Intersect(permissions)
                .Any();
        }

        private static bool HasPermission(IEnumerable<Role> userRoles, string permission)
        {
            return userRoles
                .SelectMany(role => role.Grants)
                .Select(grant => grant.Permission)
                .Contains(permission);
        }
    }
}
