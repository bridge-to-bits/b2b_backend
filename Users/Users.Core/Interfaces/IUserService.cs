using Users.Core.DTOs;
using Users.Core.Models;
using Users.Core.Responses;

namespace Users.Core.Interfaces
{
    public interface IUserService
    {
        public Task<User> Register(RegistrationDTO user);
        public Task<UserInfoResponse> GetUser(string id);
    }
}
