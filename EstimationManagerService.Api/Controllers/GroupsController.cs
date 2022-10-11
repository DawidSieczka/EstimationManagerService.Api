using EstimationManagerService.Application.Operations.Groups.Commands.CreateGroup;
using EstimationManagerService.Application.Operations.Groups.Commands.DeleteGroup;
using EstimationManagerService.Application.Operations.Groups.Queries.GetGroups;
using EstimationManagerService.Application.Operations.Groups.Queries.Models;
using Microsoft.AspNetCore.Mvc;

namespace EstimationManagerService.Api.Controllers;

public class GroupsController : ApiController
{
    [HttpGet("{companyExternalId}")]
    public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroupsAsync(Guid companyExternalId)
    {
        var groups = await Mediator.Send(new GetGroupsQuery{ CompanyExternalId = companyExternalId});
        return Ok(groups);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateGroupAsync(CreateGroupCommand createGroupCommand)
    {
        var groupExternalId = await Mediator.Send(createGroupCommand);
        return Created(GetCreatedRoute(nameof(GroupsController), groupExternalId), groupExternalId);
    }

    [HttpDelete("{externalId}")]
    public async Task<ActionResult> DeleteGroupAsync(Guid externalId)
    {
        await Mediator.Send(new DeleteGroupCommand() { GroupExternalId = externalId });
        return NoContent();
    }
}