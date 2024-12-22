using Microsoft.AspNetCore.Mvc;
using Core.Responses;
using Core.DTOs.Users;
using Core.Interfaces.Services;
using Core.DTOs;
using Core.Models;

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
    [HttpGet("{userId}/performers")]
    public async Task<IActionResult> GetRelatedPerformers(Guid userId)
    {
        try
        {
            var res = await producerService.GetProducerRelatedPerformers(userId);
            return Ok(res);
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }

    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [HttpPost("{producerId}/send-agreement")]
    public async Task<IActionResult> SendAgreementEmail(string producerId, [FromBody] SendAgreementDto sendAgreementDto)
    {
        var producer = await producerService.GetProducer(Guid.Parse(producerId));

        if (producer == null)
        {
            return NotFound("Producer was not found");
        }

        foreach (var performerId in sendAgreementDto.PerformerIds)
        {
            var performer = await performerService.GetPerformer(Guid.Parse(performerId));
            if (performer == null)
            {
                return NotFound("Performer was not found");
            }
            await mailService.SendAgreementEmailAsync(producerId, producer.User.Username, performerId, performer.User.Email);
        }
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