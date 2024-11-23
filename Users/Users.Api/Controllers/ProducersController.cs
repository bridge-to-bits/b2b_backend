using Microsoft.AspNetCore.Mvc;
using Users.Core.DTOs;
using Users.Core.Interfaces;
using Users.Core.Responses;

namespace Users.Api.Controllers;

[Route("api/producers")]
[ApiController]
public class ProducersController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<UsersResponse>> GetProducers([FromQuery] QueryAllUsersDTO queryAllUsersDTO)
    {
        var producers = await userService.GetProducers(queryAllUsersDTO);
        return Ok(producers);
    }
}