using Microsoft.AspNetCore.Mvc;
using Core.Filters;
using Core.Mapping;
using Core.Responses;
using Core.DTOs.Users;
using Core.Interfaces.Services;

namespace Api.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController (IUserService userService, IAuthService authService) : ControllerBase
{
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

    // ------------------  FAVORITE PERFORMERS ENDPOINTS SECTION   ----------------------------

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

    [HttpPost("{userId}/favorites/performers/{performerId}")]
    public async Task<IActionResult> AddFavoritePerformer(Guid userId, Guid performerId)
    {
        try
        {
            await userService.AddFavoritePerformer(userId, performerId);
            return Ok("Performer added to favorites");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{userId}/favorites/performers/{performerId}")]
    public async Task<IActionResult> RemoveFavoritePerformer(Guid userId, Guid performerId)
    {
        try
        {
            await userService.RemoveFavoritePerformer(userId, performerId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    // ------------------  FAVORITE TRACKS ENDPOINTS SECTION   ----------------------------

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
