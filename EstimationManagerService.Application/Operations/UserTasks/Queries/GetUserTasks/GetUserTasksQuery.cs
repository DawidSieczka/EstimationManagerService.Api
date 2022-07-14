using EstimationManagerService.Application.Operations.UserTasks.Queries.Models;
using EstimationManagerService.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EstimationManagerService.Application.Operations.UserTasks.Queries.GetUserTasks;

public class GetUserTasksQuery : IRequest<List<UserTaskDTO>>
{
    public Guid UserExternalId { get; set; }
}

public class GetUserTasksQueryHandler : IRequestHandler<GetUserTasksQuery, List<UserTaskDTO>>
{
    private readonly AppDbContext _appDbContext;

    public GetUserTasksQueryHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<UserTaskDTO>> Handle(GetUserTasksQuery request, CancellationToken cancellationToken)
    {
        var userEntity = await _appDbContext.Users.FirstOrDefaultAsync(x => x.ExternalId == request.UserExternalId,cancellationToken);
        if (userEntity is null) throw new Exception("User not found");

        return userEntity.UserTasks.Select(x => new UserTaskDTO()
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description
        }).ToList();
    }
}