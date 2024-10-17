using Users.Core.DTOs;
using Users.Core.Interfaces;
using Users.Core.Models;

namespace Users.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private IPasswordHasher _passwordHasher;
        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<User> Register(RegistrationDTO registrationDTO)
        {
            string hashPasword = this._passwordHasher.HashPassword(registrationDTO.Password);
            Console.WriteLine(hashPasword);

            var user = new User()
            {
                Id = Guid.NewGuid(),
                Username = registrationDTO.Username,
                Email = registrationDTO.Email,
                Password = hashPasword,
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
