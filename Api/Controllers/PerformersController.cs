using Microsoft.AspNetCore.Mvc;
using Core.Responses;
using Core.Interfaces.Services;
using Core.DTOs.Users;

namespace Api.Controllers;

[Route("api/performers")]
[ApiController]
public class PerformersController(IPerformerService performerService) : ControllerBase
{
    [ProducesResponseType(typeof(PerformersResponse) ,StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IActionResult> GetPerformers([FromQuery] QueryAllUsersDTO queryAllperformersDTO)
    {
        var performers = await performerService.GetPerformers(queryAllperformersDTO);
        return Ok(performers);
    }
}
