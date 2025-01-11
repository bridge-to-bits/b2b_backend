using Microsoft.AspNetCore.Mvc;
using Core.Interfaces.Services;
using Core.DTOs.Users;
using Core.Responses.Performers;
using Swashbuckle.AspNetCore.Annotations;
using Core.Mapping;

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
        var performers = await userService.GetPerformers(queryAllperformersDTO);
        return Ok(performers?.ToPerformersResponse());
    }
}
