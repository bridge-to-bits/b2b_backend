using Microsoft.AspNetCore.Mvc;
using Core.DTOs.Tracks;
using Core.Interfaces.Services;
using Core.Responses;
using Core.Filters;

namespace Api.Controllers;

[Route("api/tracks")]
[ApiController]
public class TracksController(ITrackService trackService) : ControllerBase
{
    [ProducesResponseType(typeof(TracksResponse), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IActionResult> GetAllTracks([FromQuery] QueryAllTracksDTO queryAllTracksDTO)
    {
        var response = await trackService.GetTracks(queryAllTracksDTO);
        return Ok(response);
    }

    [ProducesResponseType(typeof(TrackResponse), StatusCodes.Status200OK)]
    [HttpGet("{trackId}")]
    public async Task<IActionResult> GetTrack(string trackId)
    {
        var response = await trackService.GetTrack(trackId);
        return Ok(response);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost("{trackId}/increment")]
    public async Task<IActionResult> UpdateTrackListening(Guid trackId)
    {
        await trackService.UpdateTrackListening(trackId);
        return Ok();
    }

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

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpDelete("{trackId}")]
    public async Task<IActionResult> RemoveTrack(string trackId)
    {
        await trackService.RemoveTrack(trackId);
        return Ok();
    }
}
