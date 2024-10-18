using Users.Core.DTOs;
using Users.Core.Interfaces;
using Users.Core.Models;
using Users.Core.Responses;

namespace Users.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private IPasswordHasher _passwordHasher;
        private readonly IAuthService _authService;
        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, IAuthService authService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _authService = authService;

        }
        public async Task<User> Register(RegistrationDTO registrationDTO)
        {
            string hashPasword = this._passwordHasher.HashPassword(registrationDTO.Password);

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
        public async Task<UserInfoResponse> GetUser(string id)
        {
            var user = await _userRepository.GetUser(user => user.Id.ToString() == id);
            var response = new UserInfoResponse()
            {
                Username = user.Username,
                Avatar = user.Avatar,
                FirstName = user.FirstName,
                LastName = user.LastName,
                City = user.City,
                Email = user.Email
            };
            return response;
        }

        public async Task<string> Login(LoginDTO loginDTO)
        {
            var user = await _userRepository.GetUser(user=>user.Email==loginDTO.Email);
            var check = _passwordHasher.VerifyHashedPassword(loginDTO.Password, user.Password);
            if (!check)
            {
                throw new Exception("wrong password");
            }
            var token = _authService.GenerateToken(user.Id.ToString());
            return token;
        }
    }
}
