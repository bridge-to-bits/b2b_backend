using Microsoft.AspNetCore.Mvc;
using Core.Filters;
using Core.Mapping;
using Core.DTOs.Users;
using Core.Interfaces.Services;
using Core.Responses.Users;
using Core.Responses.Performers;
using Core.Utils;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController (IUserService userService, IAuthService authService) : ControllerBase
{
    [SwaggerOperation(Summary = "Get user by passed token")]
    [ProducesResponseType(typeof(GetMeResponse), 200)]
    [ProducesResponseType(404)]
    [HttpGet("me")]
    [TokenAuthorize]
    public async Task<IActionResult> GetMe()
    {
        var userId = HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId == null)
            return Unauthorized(new { message = "userId not found in token" });

        var user = await userService.GetMe(userId);

        if (user == null)
            return NotFound(new { message = "User not found" });

        return Ok(user.ToGetMeResponse());
    }

    [SwaggerOperation(Summary = "User registration endpoint")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(400)]
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

    [SwaggerOperation(Summary = "Update user profile")]
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

    [SwaggerOperation(Summary = "Get user profile")]
    [ProducesResponseType(typeof(ProfileResponse), 200)]
    [HttpGet("{userId}/profile")]
    public async Task<IActionResult> GetUserProfile([FromRoute] string userId)
    {
        var profile = await userService.GetUserProfile(userId);
        return Ok(profile);
    }

    [SwaggerOperation(Summary = "Login via user creds")]
    [ProducesResponseType(typeof(string), 200)]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        var token = await userService.Login(loginDTO);
        return Ok(token);
    }

    [SwaggerOperation(Summary = "Set some specific permission to user")]
    [ProducesResponseType(200)]
    [AuthorizePermission("setPermissions")]
    [HttpPost("{userId}/setPermissions")]
    public async Task<IActionResult> SetPermissions(
        [FromBody] SetPermissionsDTO permissionsDTO,
        string userId)
    {
        await authService.SetPermissions(userId, permissionsDTO.Permissions);

        return Ok();
    }

    [SwaggerOperation(Summary = "Get full user information")]
    [ProducesResponseType(typeof(UserInfoResponse),200)]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(string userId)
    {
        return Ok(await userService.GetUser(userId));
    }

    [SwaggerOperation(Summary = "Add rating to target user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [AuthorizePermission("addRating")]
    [HttpPost("{targetUserId}/rate")]
    [TokenAuthorize]
    public async Task<IActionResult> AddRating(
        [FromBody] AddRatingDTO addRatingDTO,
        string targetUserId)
    {
        var initiatorUserId = HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (initiatorUserId == null)
            return Unauthorized(new { message = "userId not found in token" });

        if (initiatorUserId == targetUserId)
        {
            return BadRequest(new { message = "You can not rate yourself :)" });
        }

        await userService.AddRating(addRatingDTO, targetUserId, initiatorUserId);
        return Ok();
    }
}
