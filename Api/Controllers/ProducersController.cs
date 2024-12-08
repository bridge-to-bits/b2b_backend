using Microsoft.AspNetCore.Mvc;
using Core.DTOs;
using Core.Interfaces;
using Core.Responses;

namespace Api.Controllers;

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