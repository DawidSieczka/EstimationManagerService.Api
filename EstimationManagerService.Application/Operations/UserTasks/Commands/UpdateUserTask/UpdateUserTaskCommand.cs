using EstimationManagerService.Application.Common.Exceptions;
using EstimationManagerService.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EstimationManagerService.Application.Operations.UserTasks.Commands.UpdateUserTask;

public class UpdateUserTaskCommand : IRequest
{
    public Guid UserTaskExternalId { get; set; }
    public string DisplayName { get; set; }
    public string Description { get; set; }
}

public class UpdateUserTaskCommandHandler : IRequestHandler<UpdateUserTaskCommand>
{
    private readonly AppDbContext _dbContext;

    public UpdateUserTaskCommandHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Unit> Handle(UpdateUserTaskCommand request, CancellationToken cancellationToken)
    {
        var userTaskEntity =
            await _dbContext.UserTasks.FirstOrDefaultAsync(x => x.ExternalId == request.UserTaskExternalId,
                cancellationToken);
        if (userTaskEntity is null) throw new NotFoundException("User task", request.UserTaskExternalId);

        userTaskEntity.DisplayName = request.DisplayName;
        userTaskEntity.Description = request.Description;

        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}