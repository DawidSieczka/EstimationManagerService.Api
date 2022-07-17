using EstimationManagerService.Application.Operations.Users.Commands.CreateUser;
using EstimationManagerService.Application.Operations.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EstimationManagerService.Api.Controllers;

/// <summary>
/// Users details endpoint.
/// </summary>
public class UsersController : ApiController
{

    /// <summary>
    /// Gets all users. Admin access only.
    /// </summary>
    /// <param name="page">Page number to fetch data from.</param>
    /// <param name="pageSize">Amount of data per page.</param>
    /// <returns>List of users.</returns>
    [SwaggerResponse(StatusCodes.Status200OK, "Returns paginated users", typeof(Guid))]
    [HttpGet]
    public async Task<IActionResult> GetUsersAsync(int page = 1, int pageSize = 10)
    {
        var paginatedUsers = await Mediator.Send(new GetUsersQuery()
        {
            Page = page,
            PageSize = pageSize,
        });

        return Ok(paginatedUsers);
    }

    /// <summary>
    /// Creates user in the application scope.
    /// </summary>
    /// <param name="createUserCommand">User model.</param>
    /// <returns>User external id.</returns>
    [SwaggerResponse(StatusCodes.Status200OK, "Returns created user's external id", typeof(Guid))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Incompletes the request due to invalid body.")]
    [HttpPost]
    public async Task<IActionResult> CreateUserAsync(CreateUserCommand createUserCommand)
    {
        var externalUserId = await Mediator.Send(createUserCommand);
        var createdRoute = GetCreatedRoute(nameof(CompaniesController), externalUserId);

        return Created(createdRoute, externalUserId);
    }
}
