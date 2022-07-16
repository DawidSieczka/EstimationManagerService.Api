using EstimationManagerService.Application.Operations.UserTasks.Queries.GetUserTasks;
using EstimationManagerService.Application.Operations.UserTasks.Queries.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EstimationManagerService.Api.Controllers;

/// <summary>
/// User's tasks endpoint.
/// </summary>
public class UserTasksController : ApiController
{
    /// <summary>
    /// Gets a collection of tasks for specific user.
    /// </summary>
    /// <param name="externalId">User's external id.</param>
    /// <returns>Collection of users tasks.</returns>
    [SwaggerResponse(StatusCodes.Status200OK, "Received a collection of user's tasks", typeof(List<UserTaskDTO>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User external id not found. The collection not received.", typeof(List<UserTaskDTO>))]
    [HttpGet("{externalId}")]
    public async Task<IActionResult> GetUserTasks(Guid externalId)
    {
        var userTasks = await Mediator.Send(new GetUserTasksQuery() { UserExternalId = externalId });
        return Ok(userTasks);
    }
}
