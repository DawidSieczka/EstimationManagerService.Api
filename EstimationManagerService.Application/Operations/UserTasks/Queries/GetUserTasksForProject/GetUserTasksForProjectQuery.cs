using EstimationManagerService.Application.Common.Exceptions;
using EstimationManagerService.Application.Operations.UserTasks.Queries.Models;
using EstimationManagerService.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EstimationManagerService.Application.Operations.UserTasks.Queries.GetUserTasksForProject;

public class GetUserTasksForProjectQuery : IRequest<IEnumerable<UserTaskDTO>>
{
    public Guid ProjectExternalId { get; set; }
    public Guid UserExternalId { get; set; }
}

public class GetUserTasksForProjectQueryHandler : IRequestHandler<GetUserTasksForProjectQuery, IEnumerable<UserTaskDTO>>
{
    private readonly AppDbContext _dbContext;

    public GetUserTasksForProjectQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<UserTaskDTO>> Handle(GetUserTasksForProjectQuery request,
        CancellationToken cancellationToken)
    {
        var projectEntity =
            await _dbContext.Projects.FirstOrDefaultAsync(x => x.ExternalId == request.ProjectExternalId,
                cancellationToken);

        if (projectEntity is null) throw new NotFoundException("Project", request.ProjectExternalId);

        var userEntity =
            await _dbContext.Users.FirstOrDefaultAsync(x => x.ExternalId == request.UserExternalId, cancellationToken);

        if (userEntity is null) throw new NotFoundException("User", request.UserExternalId);

        var userTasksEntities = projectEntity.Tasks.Where(x => x.UserId == userEntity.Id);

        return userTasksEntities.Select(x => new UserTaskDTO()
        {
            ExternalId = x.ExternalId,
            Name = x.DisplayName,
            Description = x.Description
        });
    }
}