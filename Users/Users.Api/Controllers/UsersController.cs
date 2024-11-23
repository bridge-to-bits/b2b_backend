using Microsoft.AspNetCore.Mvc;
using Users.Core.DTOs;
using Users.Core.Filters;
using Users.Core.Interfaces;
using Users.Core.Responses;

namespace Users.Api.Controllers;

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

    [HttpGet("register/availableGenres")]
    public async Task<IActionResult> GetAvailableGenres()
    {
        var genres = await userService.GetAvailableGenres();
        return Ok(genres);
    }

    [HttpPost("register/availableGenres")]
    public async Task<IActionResult> AddAvailableGenre([FromBody] AddGenreDTO addGenreDTO)
    {
        var result = await userService.AddAvailableGenre(addGenreDTO);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        var token = await userService.Login(loginDTO);
        return Ok(token);
    }

    [HttpPost("{userId}/setPermissions")]
    [AuthorizePermission("setPermissions")]
    public async Task<IActionResult> SetPermissions(
        [FromBody] SetPermissionsDTO permissionsDTO,
        string userId)
    {
        await authService.SetPermissions(userId, permissionsDTO.Permissions);

        return Ok();
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(string userId)
    {
        return Ok(await userService.GetUser(userId));
    }

    [HttpPost("{targetUserId}/addRating")]
    [AuthorizePermission("addRating")]
    public async Task<IActionResult> AddRating(
        [FromBody] AddRatingDTO addRatingDTO, 
        string targetUserId)
    {
        await userService.AddRating(addRatingDTO, targetUserId);
        return Ok();
    }
}
