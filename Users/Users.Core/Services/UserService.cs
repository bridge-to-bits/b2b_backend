using Users.Core.DTOs;
using Users.Core.Interfaces;
using Users.Core.Models;

namespace Users.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> Register(RegistrationDTO registrationDTO)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Username = registrationDTO.Username,
                Email = registrationDTO.Email,
                Password = registrationDTO.Password,  //Create PasswordHasher
                FirstName = registrationDTO.FirstName,
                LastName = registrationDTO.LastName,
                City = registrationDTO.City,
                Age = registrationDTO.Age,
                Avatar = registrationDTO.Avatar,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            await _userRepository.CreateUser(user);

            return user;
        }
    }
}
