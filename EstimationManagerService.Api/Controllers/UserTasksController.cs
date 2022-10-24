using EstimationManagerService.Application.Operations.UserTasks.Commands.CreateUserTask;
using EstimationManagerService.Application.Operations.UserTasks.Commands.DeleteUserTask;
using EstimationManagerService.Application.Operations.UserTasks.Commands.UpdateUserTask;
using EstimationManagerService.Application.Operations.UserTasks.Queries.GetUserTasks;
using EstimationManagerService.Application.Operations.UserTasks.Queries.GetUserTasksForProject;
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
    /// <param name="userExternalId">User's external id.</param>
    /// <returns>Collection of users tasks.</returns>
    [SwaggerResponse(StatusCodes.Status200OK, "Received a collection of user's tasks", typeof(IEnumerable<UserTaskDTO>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found. The collection not received.", typeof(IEnumerable<UserTaskDTO>))]
    [HttpGet("{userExternalId}")]
    public async Task<ActionResult<IEnumerable<UserTaskDTO>>> GetUserTasksAsync(Guid userExternalId)
    {
        var userTasks = await Mediator.Send(new GetAllUserTasksQuery() { UserExternalId = userExternalId });
        return Ok(userTasks);
    }

    /// <summary>
    /// Get a collection user tasks for specific project.
    /// </summary>
    /// <param name="userExternalId">User's external id.</param>
    /// <param name="projectExternalId">Project's external id.</param>
    /// <returns>Collection of users tasks for specific project.</returns>
    [SwaggerResponse(StatusCodes.Status200OK, "Received a collection of user's tasks", typeof(IEnumerable<UserTaskDTO>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found. The collection not received.", typeof(IEnumerable<UserTaskDTO>))]
    [HttpGet("{userExternalId}/projectExternalId/{projectExternalId}")]
    public async Task<ActionResult<IEnumerable<UserTaskDTO>>> GetUserTasksForProjectAsync(Guid userExternalId,
        Guid projectExternalId)
    {
        var userTasks = await Mediator.Send(new GetUserTasksForProjectQuery()
        {
            ProjectExternalId = projectExternalId,
            UserExternalId = userExternalId
        });

        return Ok(userTasks);
    }

    /// <summary>
    /// Creates user task.
    /// </summary>
    /// <param name="createUserTaskCommand">User task model.</param>
    /// <returns>User task external id.</returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateUserTaskAsync(CreateUserTaskCommand createUserTaskCommand)
    {
        var userTaskExternalId = await Mediator.Send(createUserTaskCommand);

        var createdRoute = GetCreatedRoute(nameof(UserTasksController), userTaskExternalId);
        return Created(createdRoute, userTaskExternalId);
    }

    /// <summary>
    /// Updates user task.
    /// </summary>
    /// <param name="updateUserTaskCommand">User task model</param>
    /// <returns>No content.</returns>
    [HttpPut]
    public async Task<ActionResult> UpdateUserTaskAsync(UpdateUserTaskCommand updateUserTaskCommand)
    {
        await Mediator.Send(updateUserTaskCommand);
        return NoContent();
    }

    /// <summary>
    /// Deletes user task.
    /// </summary>
    /// <param name="userTaskExternalId">User task external id.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{userTaskExternalId}")]
    public async Task<ActionResult> DeleteUserTaskAsync(Guid userTaskExternalId)
    {
        await Mediator.Send(new DeleteUserTaskCommand() { UserTaskExternalId = userTaskExternalId });
        return NoContent();
    }
}