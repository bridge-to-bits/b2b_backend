using Microsoft.AspNetCore.Mvc;
using Core.Filters;
using Core.Mapping;
using Core.DTOs.Users;
using Core.Interfaces.Services;
using Core.Responses.Users;
using Core.Responses.Tracks;
using Core.Responses.Performers;
using Core.Utils;

namespace Api.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController (IUserService userService, IAuthService authService) : ControllerBase
{
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

    [ProducesResponseType(typeof(ProfileResponse), 200)]
    [HttpGet("{userId}/profile")]
    public async Task<IActionResult> GetUserProfile([FromRoute] string userId)
    {
        var profile = await userService.GetUserProfile(userId);
        return Ok(profile);
    }

    [ProducesResponseType(typeof(string), 200)]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        var token = await userService.Login(loginDTO);
        return Ok(token);
    }

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

    [ProducesResponseType(typeof(UserInfoResponse),200)]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(string userId)
    {
        return Ok(await userService.GetUser(userId));
    }

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

        if(initiatorUserId == targetUserId)
        {
            return BadRequest(new { message = "You can not rate yourself :)" });
        }

        await userService.AddRating(addRatingDTO, targetUserId, initiatorUserId);
        return Ok();
    }

    // ------------------  FAVORITE PERFORMERS ENDPOINTS SECTION   ----------------------------

    [ProducesResponseType(typeof(IEnumerable<FavoritePerformerResponse>), 200)]
    [HttpGet("{userId}/favorites/performers")]
    public async Task<IActionResult> GetFavoritePerformers(Guid userId)
    {
        try
        {
            var res = await userService.GetFavoritePerformers(userId);
            return Ok(res);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [ProducesResponseType(typeof(IEnumerable<IsFavoritePerformerResponse>), 200)]
    [HttpGet("{userId}/favorites/performers/{performerUserId}")]
    [ServiceFilter(typeof(PerformerToUserPipe))]
    public async Task<IActionResult> GetIsFavoritePerformer(Guid userId, Guid performerUserId)
    {
        var performerId = (Guid)HttpContext.Items["ResolvedPerformerId"]!;
        var res = await userService.IsFavoritePerformer(userId, performerId);
        return Ok(res);
    }

    [ProducesResponseType(typeof(string), 200)]
    [HttpPost("favorites/performers/{performerUserId}")]
    [TokenAuthorize]
    [ServiceFilter(typeof(PerformerToUserPipe))]
    public async Task<IActionResult> AddFavoritePerformer(Guid performerUserId)
    {
        var initiatorUserId = HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (initiatorUserId == null)
            return Unauthorized(new { message = "userId not found in token" });

        var performerId = (Guid)HttpContext.Items["ResolvedPerformerId"]!;

        try
        {
            await userService.AddFavoritePerformer(Guid.Parse(initiatorUserId), performerId);
            return Ok("Performer added to favorites");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [ProducesResponseType(200)]
    [HttpDelete("favorites/performers/{performerUserId}")]
    [TokenAuthorize]
    [ServiceFilter(typeof(PerformerToUserPipe))]
    public async Task<IActionResult> RemoveFavoritePerformer(Guid performerUserId)
    {
        var initiatorUserId = HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (initiatorUserId == null)
            return Unauthorized(new { message = "userId not found in token" });

        var performerId = (Guid)HttpContext.Items["ResolvedPerformerId"]!;

        try
        {
            await userService.RemoveFavoritePerformer(Guid.Parse(initiatorUserId), performerId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    // ------------------  FAVORITE TRACKS ENDPOINTS SECTION   ----------------------------

    [ProducesResponseType(typeof(IEnumerable<FavoriteTrackResponse>), 200)]
    [HttpGet("{userId}/favorites/tracks")]
    public async Task<IActionResult> GetFavoriteTracks(Guid userId)
    {
        try
        {
            var res = await userService.GetFavoriteTracks(userId);
            return Ok(res);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{userId}/favorites/tracks/{trackId}")]
    public async Task<IActionResult> AddFavoriteTrack(Guid userId, Guid trackId)
    {
        try
        {
            await userService.AddFavoriteTrack(userId, trackId);
            return Ok("Track added to favorites");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{userId}/favorites/tracks/{trackId}")]
    public async Task<IActionResult> RemoveFavoriteTrack(Guid userId, Guid trackId)
    {
        try
        {
            await userService.RemoveFavoriteTrack(userId, trackId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
