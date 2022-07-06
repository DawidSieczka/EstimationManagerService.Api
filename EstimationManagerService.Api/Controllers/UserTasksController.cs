using EstimationManagerService.Application.Operations.UserTasks.Queries.GetUserTasks;
using Microsoft.AspNetCore.Mvc;

namespace EstimationManagerService.Api.Controllers;


public class UserTasksController : ApiController
{
    [HttpGet("{externalId}")]
    public async Task<IActionResult> GetUserTasks(Guid externalId)
    {
        var userTasks = await Mediator.Send(new GetUserTasksQuery() { UserExternalId = externalId });
        return Ok(userTasks);
    }
}
