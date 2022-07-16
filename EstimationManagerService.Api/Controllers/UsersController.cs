using EstimationManagerService.Application.Operations.Users.Commands.CreateUser;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EstimationManagerService.Api.Controllers;

/// <summary>
/// Users details endpoint.
/// </summary>
public class UsersController : ApiController
{
    /// <summary>
    /// Creates user in the application scope.
    /// </summary>
    /// <param name="createUserCommand">User model.</param>
    /// <returns>User external id.</returns>
    [SwaggerResponse(StatusCodes.Status200OK, "Returns created user's external id", typeof(Guid))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Incompletes the request due to invalid body.")]
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserCommand createUserCommand)
    {
        var userId = await Mediator.Send(createUserCommand);
        return Ok(userId);
    }
}
