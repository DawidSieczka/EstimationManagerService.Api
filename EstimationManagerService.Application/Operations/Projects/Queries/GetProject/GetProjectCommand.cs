using EstimationManagerService.Application.Common.Exceptions;
using EstimationManagerService.Application.Operations.Projects.Queries.Models;
using EstimationManagerService.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EstimationManagerService.Application.Operations.Projects.Queries.GetProject;

public class GetProjectCommand : IRequest<ProjectDto>
{
    public Guid ProjectExternalId { get; set; }
}

public class GetProjectCommandHandler : IRequestHandler<GetProjectCommand, ProjectDto>
{
    private readonly AppDbContext _dbContext;

    public GetProjectCommandHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProjectDto> Handle(GetProjectCommand request, CancellationToken cancellationToken)
    {
        var projectEntity = await _dbContext.Projects.FirstOrDefaultAsync(x => x.ExternalId == request.ProjectExternalId,
            cancellationToken);

        if (projectEntity is null) throw new NotFoundException("Project", request.ProjectExternalId);

        return new ProjectDto()
        {
            DisplayName = projectEntity.DisplayName,
            ExternalId = projectEntity.ExternalId
        };
    }
}