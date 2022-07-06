using System.ComponentModel.DataAnnotations;

namespace EstimationManagerService.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public Guid ExternalId { get; set; }

    [MaxLength(30)]
    public string DisplayName { get; set; }

    public virtual List<UserTask> UserTasks { get; set; }
}