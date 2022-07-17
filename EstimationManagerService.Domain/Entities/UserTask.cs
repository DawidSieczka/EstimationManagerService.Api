using System.ComponentModel.DataAnnotations;
using EstimationManagerService.Domain.EntitiesConfigurations;

namespace EstimationManagerService.Domain.Entities;

public class UserTask
{
    public int Id { get; set; }

    public Guid ExternalId { get; set; }
    [MaxLength(EntityConfigurationValues.DisplayNameMaximumLenth)]
    [MinLength(EntityConfigurationValues.DisplayNameMinimumLenth)]
    public string DisplayName { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }
    public virtual Project Project { get; set; }
    public int ProjectId { get; set; }
}