using System.ComponentModel.DataAnnotations;

namespace EstimationManagerService.Domain.Entities;

public class UserTask
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }
}