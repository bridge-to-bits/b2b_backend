using Microsoft.AspNetCore.Mvc;
using Users.Core.DTOs;
using Users.Core.Interfaces;
using Users.Core.Responses;

namespace Users.Api.Controllers;

[Route("api/performers")]
[ApiController]
public class PerformersController (IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<UsersResponse>> GetPerformers([FromQuery] QueryAllUsersDTO queryAllUsersDTO)
    {
        var performers = await userService.GetPerformers(queryAllUsersDTO);
        return Ok(performers);
    }
}
