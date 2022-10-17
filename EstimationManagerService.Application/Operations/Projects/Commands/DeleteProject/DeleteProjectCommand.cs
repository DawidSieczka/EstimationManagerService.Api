using EstimationManagerService.Application.Common.Exceptions;
using EstimationManagerService.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace EstimationManagerService.Application.Operations.Projects.Commands.DeleteProject;

public class DeleteProjectCommand : IRequest
{
    public Guid ProjectExternalId { get; set; }
}

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
{
    private readonly AppDbContext _dbContext;

    public DeleteProjectCommandHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var projectEntity = await _dbContext.Projects.FirstOrDefaultAsync(x => x.ExternalId == request.ProjectExternalId,
                cancellationToken);

        if (projectEntity is null) throw new NotFoundException("Project", request.ProjectExternalId);

        _dbContext.Projects.Remove(projectEntity);

        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}