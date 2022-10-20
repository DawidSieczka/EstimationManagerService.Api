using EstimationManagerService.Application.Operations.UserTasks.Queries.Models;
using EstimationManagerService.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EstimationManagerService.Application.Operations.UserTasks.Queries.GetUserTasks;

public class GetAllUserTasksQuery : IRequest<IEnumerable<UserTaskDTO>>
{
    public Guid UserExternalId { get; set; }
}

public class GetUserTasksQueryHandler : IRequestHandler<GetAllUserTasksQuery, IEnumerable<UserTaskDTO>>
{
    private readonly AppDbContext _appDbContext;

    public GetUserTasksQueryHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<UserTaskDTO>> Handle(GetAllUserTasksQuery request, CancellationToken cancellationToken)
    {
        var userEntity = await _appDbContext.Users.FirstOrDefaultAsync(x => x.ExternalId == request.UserExternalId, cancellationToken);
        if (userEntity is null) throw new Exception("User not found");

        return userEntity.UserTasks.Select(x => new UserTaskDTO()
        {
            ExternalId = x.ExternalId,
            Name = x.DisplayName,
            Description = x.Description
        });
    }
}