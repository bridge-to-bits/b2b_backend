using Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Core.Utils;

public class PerformerToUserPipe(IPerformerRepository performerRepository) : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ActionArguments.TryGetValue("performerUserId", out var performerIdObj) ||
            performerIdObj is not Guid performerUserId)
        {
            context.Result = new BadRequestObjectResult(new { message = "Invalid performerUserId" });
            return;
        }

        var performer = await performerRepository.GetPerfomer(performer => performer.UserId == performerUserId);
        if (performer == null)
        {
            context.Result = new NotFoundObjectResult(new { message = "Performer not found" });
            return;
        }

        context.HttpContext.Items["ResolvedPerformerId"] = performer.Id;

        await next();
    }
}

