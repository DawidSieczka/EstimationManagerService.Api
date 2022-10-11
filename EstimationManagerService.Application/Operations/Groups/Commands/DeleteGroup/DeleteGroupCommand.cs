using EstimationManagerService.Application.Common.Exceptions;
using EstimationManagerService.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EstimationManagerService.Application.Operations.Groups.Commands.DeleteGroup;

public class DeleteGroupCommand : IRequest
{
    public Guid GroupExternalId { get; set; }
}

public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand>
{
    private readonly AppDbContext _dbContext;

    public DeleteGroupCommandHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var groupEntity =
            await _dbContext.Groups.FirstOrDefaultAsync(x => x.ExternalId == request.GroupExternalId,
                cancellationToken);

        if (groupEntity is null)
            throw new NotFoundException("Group", request.GroupExternalId);

        _dbContext.Groups.Remove(groupEntity);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}