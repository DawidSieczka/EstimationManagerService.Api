using EstimationManagerService.Application.Operations.Projects.Commands.CreateProject;
using EstimationManagerService.Application.Operations.Projects.Commands.DeleteProject;
using EstimationManagerService.Application.Operations.Projects.Commands.UpdateProject;
using EstimationManagerService.Application.Operations.Projects.Queries.GetProject;
using EstimationManagerService.Application.Operations.Projects.Queries.GetProjects;
using EstimationManagerService.Application.Operations.Projects.Queries.Models;
using Microsoft.AspNetCore.Mvc;

namespace EstimationManagerService.Api.Controllers;

public class ProjectsController : ApiController
{
    /// <summary>
    /// Gets list of projects under a specific group.
    /// </summary>
    /// <param name="groupExternalId">Group external Id type of Guid.</param>
    /// <returns>List of projects.</returns>
    [HttpGet("{groupExternalId}")]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAllProjectsInGroupAsync(Guid groupExternalId)
    {
        var projects = await Mediator.Send(new GetProjectsQuery() { GroupExternalId = groupExternalId });
        return Ok(projects);
    }

    /// <summary>
    /// Gets specific project details.
    /// </summary>
    /// <param name="projectExternalId">Project external Id type of Guid.</param>
    /// <returns>Project details.</returns>
    [HttpGet("{projectExternalId}/details")]
    public async Task<ActionResult<ProjectDto>> GetProjectDetailsAsync(Guid projectExternalId)
    {
        var project = await Mediator.Send(new GetProjectCommand() { ProjectExternalId = projectExternalId });
        return Ok(project);
    }

    /// <summary>
    /// Creates a new project.
    /// </summary>
    /// <param name="createProjectCommand">Project create model.</param>
    /// <returns>Project external id.</returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateProjectAsync(CreateProjectCommand createProjectCommand)
    {
        var projectExternalId = await Mediator.Send(createProjectCommand);
        return projectExternalId;
    }

    /// <summary>
    /// Deletes specific project.
    /// </summary>
    /// <param name="projectExternalId">Project external Id type of Guid.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{projectExternalId}")]
    public async Task<ActionResult> DeleteProjectAsync(Guid projectExternalId)
    {
        await Mediator.Send(new DeleteProjectCommand() { ProjectExternalId = projectExternalId });
        return NoContent();
    }

    /// <summary>
    /// Updates project details.
    /// </summary>
    /// <param name="updateProjectCommand">Project update model.</param>
    /// <returns>No content.</returns>
    [HttpPut]
    public async Task<ActionResult> UpdateProjectDetailsAsync(UpdateProjectCommand updateProjectCommand)
    {
        await Mediator.Send(updateProjectCommand);
        return NoContent();
    }
}