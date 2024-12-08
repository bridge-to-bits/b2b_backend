using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.DTOs.Tracks;

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

    [HttpPost]
    public async Task<IActionResult> UploadTracks([FromBody] UploadTracksDTO uploadTrackDTO)
    {
        var response = await trackService.UploadTracks(uploadTrackDTO);
        return Ok(response);
    }

    [HttpDelete("{trackId}")]
    public async Task<IActionResult> RemoveTrack(string trackId)
    {
        await trackService.RemoveTrack(trackId);
        return Ok();
    }
}
