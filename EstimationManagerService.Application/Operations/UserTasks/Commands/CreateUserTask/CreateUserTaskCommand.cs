using EstimationManagerService.Application.Common.Exceptions;
using EstimationManagerService.Application.Common.Helpers.MockingHelpers.Interfaces;
using EstimationManagerService.Domain.Entities;
using EstimationManagerService.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EstimationManagerService.Application.Operations.UserTasks.Commands.CreateUserTask;

public class CreateUserTaskCommand : IRequest<Guid>
{
    public string DisplayName { get; set; }
    public string Description { get; set; }
    public DateTime? TaskStartDate { get; set; }
    public TimeSpan? TaskEstimationTime { get; set; }
    public Guid ProjectExternalId { get; set; }
    public Guid UserExternalId { get; set; }
}

public class CreateUserTaskCommandHandler : IRequestHandler<CreateUserTaskCommand, Guid>
{
    private readonly AppDbContext _dbContext;
    private readonly IGuidHelper _guidHelper;

    public CreateUserTaskCommandHandler(AppDbContext dbContext, IGuidHelper guidHelper)
    {
        _dbContext = dbContext;
        _guidHelper = guidHelper;
    }

    public async Task<Guid> Handle(CreateUserTaskCommand request, CancellationToken cancellationToken)
    {
        var projectEntity =
            await _dbContext.Projects.FirstOrDefaultAsync(x => x.ExternalId == request.ProjectExternalId,
                cancellationToken);
        if (projectEntity is null) throw new NotFoundException("Project", request.ProjectExternalId);

        var userEntity =
            await _dbContext.Users.FirstOrDefaultAsync(x => x.ExternalId == request.UserExternalId, cancellationToken);
        if (userEntity is null) throw new NotFoundException("User", request.UserExternalId);

        var userTaskEntry = await _dbContext.UserTasks.AddAsync(new UserTask()
        {
            ExternalId = _guidHelper.CreateGuid(),
            DisplayName = request.DisplayName,
            Description = request.Description,
            TaskStartDate = request.TaskStartDate,
            TaskEndDate = null,
            TaskEstimationTime = request.TaskEstimationTime,
            TaskTimeDetails = new List<TaskTimeDetails>(),
            ProjectId = projectEntity.Id,
            UserId = userEntity.Id
        }, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return userTaskEntry.Entity.ExternalId;
    }
}