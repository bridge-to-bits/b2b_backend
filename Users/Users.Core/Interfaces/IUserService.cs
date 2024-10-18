using Users.Core.DTOs;
using Users.Core.Models;

namespace Users.Core.Interfaces
{
    public interface IUserService
    {
        public Task<User> Register(RegistrationDTO user);
        public Task<string> Login(LoginDTO user);
    }
}
