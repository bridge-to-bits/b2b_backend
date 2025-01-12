using Microsoft.AspNetCore.Mvc;
using Core.DTOs.Tracks;
using Core.Interfaces.Services;
using Core.Filters;
using Core.Responses.Tracks;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[Route("api/tracks")]
[ApiController]
public class TracksController(ITrackService trackService) : ControllerBase
{
    [SwaggerOperation(Summary = "Get all tracks")]
    [ProducesResponseType(typeof(TracksResponse), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IActionResult> GetAllTracks([FromQuery] QueryAllTracksDTO queryAllTracksDTO)
    {
        var response = await trackService.GetTracks(queryAllTracksDTO);
        return Ok(response);
    }

    [SwaggerOperation(Summary = "Get specific track by id")]
    [ProducesResponseType(typeof(TrackResponse), StatusCodes.Status200OK)]
    [HttpGet("{trackId}")]
    public async Task<IActionResult> GetTrack(string trackId)
    {
        var response = await trackService.GetTrack(trackId);
        return Ok(response);
    }

    [SwaggerOperation(Summary = "Increase track listenings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost("{trackId}/increment")]
    public async Task<IActionResult> UpdateTrackListening(Guid trackId)
    {
        await trackService.UpdateTrackListening(trackId);
        return Ok();
    }

    [SwaggerOperation(Summary = "upload track")]
    [ProducesResponseType(typeof(TrackResponse), StatusCodes.Status200OK)]
    [TokenAuthorize]
    [HttpPost]
    public async Task<IActionResult> UploadTracks([FromForm] UploadTrackDTO uploadTrackDTO)
    {
        var userId = HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId == null)
            return Unauthorized(new { message = "userId not found in token" });

        try
        {
            var response = await trackService.UploadTrack(uploadTrackDTO, userId);
            return Ok(response);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [SwaggerOperation(Summary = "Delete track")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpDelete("{trackId}")]
    public async Task<IActionResult> RemoveTrack(string trackId)
    {
        await trackService.RemoveTrack(trackId);
        return Ok();
    }


    // ------------------  FAVORITE TRACKS ENDPOINTS SECTION   ----------------------------


    [SwaggerOperation(Summary = "Get favorite tracks")]
    [ProducesResponseType(typeof(IEnumerable<FavoriteTrackResponse>), 200)]
    [HttpGet("favorites")]
    [TokenAuthorize]
    public async Task<IActionResult> GetFavoriteTracks()
    {
        var userId = HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId == null)
            return Unauthorized(new { message = "userId not found in token" });

        var res = await trackService.GetFavoriteTracks(Guid.Parse(userId));
        return Ok(res);
    }

    [SwaggerOperation(Summary = "Checks if a track is marked as favorite for the specified user")]
    [ProducesResponseType(typeof(IEnumerable<IsFavoriteTrackResponse>), 200)]
    [HttpGet("favorites/{trackId}")]
    [TokenAuthorize]
    public async Task<IActionResult> GetIsFavoriteTrack(Guid trackId)
    {
        var userId = HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId == null)
            return Unauthorized(new { message = "userId not found in token" });

        var res = await trackService.IsFavoriteTrack(Guid.Parse(userId), trackId);
        return Ok(res);
    }

    [SwaggerOperation(Summary = "Add favorite track")]
    [HttpPost("favorites/{trackId}")]
    [TokenAuthorize]
    public async Task<IActionResult> AddFavoriteTrack(Guid trackId)
    {
        var userId = HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId == null)
            return Unauthorized(new { message = "userId not found in token" });

        await trackService.AddFavoriteTrack(Guid.Parse(userId), trackId);
        return Ok("Track added to favorites");
    }

    [SwaggerOperation(Summary = "Remove track from favorites")]
    [HttpDelete("favorites/{trackId}")]
    [TokenAuthorize]
    public async Task<IActionResult> RemoveFavoriteTrack(Guid trackId)
    {
        var userId = HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId == null)
            return Unauthorized(new { message = "userId not found in token" });

        await trackService.RemoveFavoriteTrack(Guid.Parse(userId), trackId);
        return NoContent();
    }
}
