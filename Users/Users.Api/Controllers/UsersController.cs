using Microsoft.AspNetCore.Mvc;
using Users.Core.DTOs;
using Users.Core.Filters;
using Users.Core.Interfaces;
using Users.Core.Models;

namespace Users.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController (IUserService userService, IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO registrationDTO)
        {
            var createdUser = await userService.Register(registrationDTO);
            return Ok(createdUser.Id.ToString());
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var token = await userService.Login(loginDTO);
            return Ok(token);
        }

        [HttpGet("test")]
        [AuthorizePermission("getInfo")]
        public async Task<IActionResult> Test()
        {
            return Ok(1);
        }

        [HttpPost("setPermissions/{userId}")]
        public async Task<IActionResult> SetPermissions(
            [FromBody] SetPermissionsDTO permissionsDTO,
            string userId
        )
        {
            await authService.SetPermissions(userId, permissionsDTO.Permissions);

            return Ok();
        }

        [HttpGet("userInfo")]
        public async Task<IActionResult> GetUser(string userId)
        {
            return Ok(await userService.GetUser(userId));
        }

        [HttpGet("getAllProducers")]
        public async Task<IActionResult> GetAllProducers(int pagenumber = 1, int pagesize = 10)
        {
            return Ok(await userService.GetAllProducers(pagenumber, pagesize));
        }
        [HttpGet("getAllPerformers")]
        public async Task<IActionResult> GetAllPerformers(int pagenumber = 1, int pagesize = 10)
        {
            return Ok(await userService.GetAllPerformers(pagenumber, pagesize));
        }
    }
}
