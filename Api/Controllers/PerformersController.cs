using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.Services;
using Core.DTOs.Users;
using Core.Responses.Performers;
using Swashbuckle.AspNetCore.Annotations;
using Core.Mapping;
using Core.Filters;
using Core.Utils;

namespace Api.Controllers;

[Route("api/performers")]
[ApiController]
public class PerformersController(IPerformerService performerService, IUserService userService) : ControllerBase
{
    [SwaggerOperation(Summary = "Get all performers")]
    [ProducesResponseType(typeof(PerformersResponse) ,StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IActionResult> GetPerformers([FromQuery] QueryAllUsersDTO queryAllperformersDTO)
    {
        var performers = await performerService.GetPerformers(queryAllperformersDTO);
        return Ok(performers);
    }

    // ------------------  FAVORITE PERFORMERS ENDPOINTS SECTION   ----------------------------


    [SwaggerOperation(Summary = "Get user favorite performers")]
    [ProducesResponseType(typeof(IEnumerable<FavoritePerformerResponse>), 200)]
    [HttpGet("favorites")]
    [TokenAuthorize]
    public async Task<IActionResult> GetFavoritePerformers()
    {
        var userId = HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId == null)
            return Unauthorized(new { message = "userId not found in token" });

        var res = await performerService.GetFavoritePerformers(Guid.Parse(userId));
        return Ok(res);
    }

    [SwaggerOperation(Summary = "Checks if a performer is marked as favorite for the specified user")]
    [ProducesResponseType(typeof(bool), 200)]
    [HttpGet("favorites/{performerUserId}")]
    [ServiceFilter(typeof(PerformerToUserPipe))]
    [TokenAuthorize]
    public async Task<IActionResult> GetIsFavoritePerformer(Guid performerUserId)
    {
        var userId = HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId == null)
            return Unauthorized(new { message = "userId not found in token" });

        var performerId = (Guid)HttpContext.Items["ResolvedPerformerId"]!;
        var res = await performerService.IsFavoritePerformer(Guid.Parse(userId), performerId);
        return Ok(res);
    }

    [SwaggerOperation(Summary = "Add performer to favorite")]
    [ProducesResponseType(typeof(string), 200)]
    [HttpPost("favorites/{performerUserId}")]
    [TokenAuthorize]
    [ServiceFilter(typeof(PerformerToUserPipe))]
    public async Task<IActionResult> AddFavoritePerformer(Guid performerUserId)
    {
        var initiatorUserId = HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (initiatorUserId == null)
            return Unauthorized(new { message = "userId not found in token" });

        var performerId = (Guid)HttpContext.Items["ResolvedPerformerId"]!;

        performerService.AddFavoritePerformer(Guid.Parse(initiatorUserId), performerId);
        return Ok("Performer added to favorites");
    }

    [SwaggerOperation(Summary = "Removes performer from favorites")]
    [ProducesResponseType(200)]
    [HttpDelete("favorites/{performerUserId}")]
    [TokenAuthorize]
    [ServiceFilter(typeof(PerformerToUserPipe))]
    public async Task<IActionResult> RemoveFavoritePerformer(Guid performerUserId)
    {
        var initiatorUserId = HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (initiatorUserId == null)
            return Unauthorized(new { message = "userId not found in token" });

        var performerId = (Guid)HttpContext.Items["ResolvedPerformerId"]!;

        performerService.RemoveFavoritePerformer(Guid.Parse(initiatorUserId), performerId);
        return NoContent();
    }
}
