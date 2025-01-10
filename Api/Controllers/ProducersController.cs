using Microsoft.AspNetCore.Mvc;
using Core.DTOs.Users;
using Core.Interfaces.Services;
using Core.DTOs;
using Core.Responses.Users;
using Core.Responses.Performers;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerOperation(Summary = "Get all producers")]
    [ProducesResponseType(typeof(UsersResponse),StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<UsersResponse>> GetProducers([FromQuery] QueryAllUsersDTO queryAllUsersDTO)
    {
        var producers = await userService.GetProducers(queryAllUsersDTO);
        return Ok(producers);
    }

    [SwaggerOperation(Summary = "Get performers related to producer")]
    [ProducesResponseType(typeof(ProducerRelatedPerformerResponse), StatusCodes.Status200OK)]
    [HttpGet("{userId}/performers")]
    public async Task<IActionResult> GetRelatedPerformers(Guid userId)
    {
        var res = await producerService.GetProducerRelatedPerformers(userId);
        return Ok(res);
    }

    [SwaggerOperation(Summary = "Send agreement email to performer")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [HttpPost("{userId}/send-agreement")]
    public async Task<IActionResult> SendAgreementEmail(Guid userId, [FromBody] SendAgreementDto sendAgreementDto)
    {
        var producer = await producerService.GetProducer(userId);

        if (producer == null)
        {
            return NotFound("Producer was not found");
        }

        foreach (var performerUserId in sendAgreementDto.PerformerIds)
        {
            var performer = await performerService.GetPerformer(performer => performer.UserId == Guid.Parse(performerUserId));
            if (performer == null)
            {
                return NotFound("Performer was not found");
            }
            await mailService.SendAgreementEmailAsync(userId.ToString(), producer.User.Username, performerUserId, performer.User.Email);
        }
        return Ok("Agreement email sent successfully.");
    }

    [SwaggerOperation(Summary = "Aprooves request from producer to performer")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [HttpGet("{producerId}/approve-agreement/{performerId}")]
    public async Task<IActionResult> ApproveAgreement(string producerId, string performerId)
    {
        var performer = await performerService.GetPerformer(performer => performer.Id == Guid.Parse(performerId));
        await producerService.AddRelatedPerformer(Guid.Parse(producerId), performer);

        return Ok("Agreement approved successfully.");
    }
}