using EstimationManagerService.Application.Common.Exceptions;
using EstimationManagerService.Application.Operations.Projects.Queries.GetProject;
using EstimationManagerService.Application.Operations.Projects.Queries.Models;
using EstimationManagerService.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EstimationManagerService.Application.Operations.Projects.Queries.GetProjects;

public class GetProjectsCommand : IRequest<IEnumerable<ProjectDto>>
{
    public Guid GroupExternalId { get; set; }
}

public class GetProjectsCommandHandler : IRequestHandler<GetProjectsCommand, IEnumerable<ProjectDto>>
{
    private readonly AppDbContext _dbContext;

    public GetProjectsCommandHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ProjectDto>> Handle(GetProjectsCommand request, CancellationToken cancellationToken)
    {
        var groupEntity = await _dbContext.Groups.FirstOrDefaultAsync(x => x.ExternalId == request.GroupExternalId,
                cancellationToken);

        if (groupEntity is null) throw new NotFoundException("Group", request.GroupExternalId);

        return groupEntity.Projects.Select(x => new ProjectDto()
        {
            DisplayName = x.DisplayName,
            ExternalId = x.ExternalId
        });
    }
}