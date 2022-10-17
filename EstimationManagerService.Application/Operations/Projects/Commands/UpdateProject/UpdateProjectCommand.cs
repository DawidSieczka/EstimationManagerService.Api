using EstimationManagerService.Application.Common.Exceptions;
using EstimationManagerService.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace EstimationManagerService.Application.Operations.Projects.Commands.UpdateProject;

public class UpdateProjectCommand : IRequest
{
    public string DisplayName { get; set; }
    public Guid ProjectExternalId { get; set; }
}

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand>
{
    private readonly AppDbContext _dbContext;

    public UpdateProjectCommandHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var projectEntity = await _dbContext.Projects.FirstOrDefaultAsync(
            x => x.ExternalId == request.ProjectExternalId,
            cancellationToken);

        if (projectEntity is null) throw new NotFoundException("Project", request.ProjectExternalId);

        projectEntity.DisplayName = request.DisplayName;

        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}