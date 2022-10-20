namespace EstimationManagerService.Application.Operations.UserTasks.Queries.Models;

public class UserTaskDTO
{
    public Guid ExternalId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}