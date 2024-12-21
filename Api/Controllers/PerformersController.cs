using Microsoft.AspNetCore.Mvc;
using Core.Responses;
using Core.Interfaces.Services;
using Core.DTOs.Performers;

namespace Api.Controllers;

[Route("api/performers")]
[ApiController]
public class PerformersController(IPerformerService performerService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<SendInvitationPerformersResponse>> GetPerformers([FromQuery] QueryAllPerformersDTO queryAllperformersDTO)
    {
        var performers = await performerService.GetPerformers(queryAllperformersDTO);
        return Ok(performers);
    }
}
