using Microsoft.AspNetCore.Mvc;
using Core.Responses;
using Core.DTOs.Users;
using Core.Interfaces.Services;

namespace Api.Controllers;

[Route("api/producers")]
[ApiController]
public class ProducersController(
    IUserService userService,
    IProducerService producerService,
    IPerformerService performerService,
    IMailService mailService
    ) : ControllerBase
{
    [ProducesResponseType(typeof(UsersResponse),StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<UsersResponse>> GetProducers([FromQuery] QueryAllUsersDTO queryAllUsersDTO)
    {
        var producers = await userService.GetProducers(queryAllUsersDTO);
        return Ok(producers);
    }

    [ProducesResponseType(typeof(ProducerRelatedPerformerResponse), StatusCodes.Status200OK)]
    [HttpGet("{producerId}/performers")]
    public async Task<IActionResult> GetRelatedPerformers(Guid producerId)
    {
        var res = await producerService.GetProducerRelatedPerformers(producerId);
        return Ok(res);
    }

    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [HttpPost("{producerId}/send-agreement/{performerId}")]
    public async Task<IActionResult> SendAgreementEmail(string producerId, string performerId)
    {
        var performer = await performerService.GetPerformer(Guid.Parse(performerId));
        var producer = await producerService.GetProducer(Guid.Parse(producerId));

        if (producer == null || performer == null)
        {
            return NotFound("perfomer or producer was not found");
        }

        await mailService.SendAgreementEmailAsync(producerId, producer.User.Username, performerId, performer.User.Email);

        return Ok("Agreement email sent successfully.");
    }

    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [HttpGet("{producerId}/approve-agreement/{performerId}")]
    public async Task<IActionResult> ApproveAgreement(string producerId, string performerId)
    {
        var performer = await performerService.GetPerformer(Guid.Parse(performerId));
        await producerService.AddRelatedPerformer(Guid.Parse(producerId), performer);

        return Ok("Agreement approved successfully.");
    }
}