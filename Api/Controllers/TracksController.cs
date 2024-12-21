using Microsoft.AspNetCore.Mvc;
using Core.DTOs.Tracks;
using Core.Interfaces.Services;

namespace Api.Controllers;

[Route("api/tracks")]
[ApiController]
public class TracksController(ITrackService trackService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllTracks([FromQuery] QueryAllTracksDTO queryAllTracksDTO)
    {
        var response = await trackService.GetTracks(queryAllTracksDTO);
        return Ok(response);
    }

    [HttpGet("{trackId}")]
    public async Task<IActionResult> GetTrack(string trackId)
    {
        var response = await trackService.GetTrack(trackId);
        return Ok(response);
    }

    [HttpPost("{trackId}/increment")]
    public async Task<IActionResult> UpdateTrackListening(Guid trackId)
    {
        await trackService.UpdateTrackListening(trackId);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> UploadTracks([FromForm] UploadTrackDTO uploadTrackDTO)
    {
        var response = await trackService.UploadTrack(uploadTrackDTO);
        return Ok(response);
    }

    [HttpDelete("{trackId}")]
    public async Task<IActionResult> RemoveTrack(string trackId)
    {
        await trackService.RemoveTrack(trackId);
        return Ok();
    }
}
