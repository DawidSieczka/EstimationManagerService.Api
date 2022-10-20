using EstimationManagerService.Application.Common.Exceptions;
using EstimationManagerService.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EstimationManagerService.Application.Operations.UserTasks.Commands.DeleteUserTask;

public class DeleteUserTaskCommand : IRequest
{
    public Guid UserTaskExternalId { get; set; }
}

public class DeleteUserTaskCommandHandler : IRequestHandler<DeleteUserTaskCommand>
{
    private readonly AppDbContext _dbContext;

    public DeleteUserTaskCommandHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(DeleteUserTaskCommand request, CancellationToken cancellationToken)
    {
        var userTaskEntity =
            await _dbContext.UserTasks.FirstOrDefaultAsync(x => x.ExternalId == request.UserTaskExternalId,
                cancellationToken);

        if (userTaskEntity is null)
            throw new NotFoundException("User Task", request.UserTaskExternalId);

        _dbContext.UserTasks.Remove(userTaskEntity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}