using MediatR;

namespace EstimationManagerService.Application.Operations.UserTasks.Commands.CreateUserTask;

public class CreateUserTaskCommand : IRequest<Guid>
{
    public string DisplayName { get; set; }
}