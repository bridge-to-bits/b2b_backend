using Microsoft.AspNetCore.Mvc;
using Core.DTOs;
using Core.Filters;
using Core.Interfaces;
using Core.Mapping;
using Core.Responses;

namespace Api.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController (IUserService userService, IAuthService authService) : ControllerBase
{
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var token = HttpContext.Request.Cookies["access_token"];

        if (string.IsNullOrEmpty(token))
            return Unauthorized(new { message = "Token not found in cookies" });

        var userId = authService.ValidateToken(token)?.Claims?.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId == null)
            return Unauthorized(new { message = "userId not found in token" });

        var user = await userService.GetMe(userId);

        if (user == null)
            return NotFound(new { message = "User not found" });

        return Ok(user.ToGetMeResponse());
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterMainInfo([FromBody] MainRegistrationDTO registrationDTO)
    {
        try
        {
            var createdUser = await userService.Register(registrationDTO);
            var token = authService.GenerateToken(createdUser.Id.ToString());
            return Ok(token);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(ProfileResponse), 200)]
    [ProducesResponseType(400)]
    [HttpPatch("{userId}/profile")]
    public async Task<IActionResult> UpdateUserProfile(
        [FromRoute] string userId,
        [FromForm] UpdateProfileDTO updateProfileDTO)
    {
        var response = await userService.UpdateUserProfile(userId, updateProfileDTO);
        return Ok(response);
    }

    [HttpGet("{userId}/profile")]
    public async Task<IActionResult> GetUserProfile([FromRoute] string userId)
    {
        var profile = await userService.GetUserProfile(userId);
        return Ok(profile);
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

    [AuthorizePermission("setPermissions")]
    [HttpPost("{userId}/setPermissions")]
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

    [AuthorizePermission("addRating")]
    [HttpPost("{targetUserId}/addRating")]
    public async Task<IActionResult> AddRating(
        [FromBody] AddRatingDTO addRatingDTO, 
        string targetUserId)
    {
        await userService.AddRating(addRatingDTO, targetUserId);
        return Ok();
    }
}
