using EstimationManagerService.Application.Operations.Users.Commands.CreateUser;
using Microsoft.AspNetCore.Mvc;

namespace EstimationManagerService.Api.Controllers;

public class UsersController : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserCommand createUserCommand)
    {
        var userId = await Mediator.Send(createUserCommand);
        return Ok(userId);
    }
}
