using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Responses;
using Core.DTOs.Users;

namespace Api.Controllers;

[Route("api/performers")]
[ApiController]
public class PerformersController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<UsersResponse>> GetPerformers([FromQuery] QueryAllUsersDTO queryAllUsersDTO)
    {
        var performers = await userService.GetPerformers(queryAllUsersDTO);
        return Ok(performers);
    }
}
