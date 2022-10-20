using System.ComponentModel.DataAnnotations;
using EstimationManagerService.Domain.EntitiesConfigurations;

namespace EstimationManagerService.Domain.Entities;

public class UserTask
{
    public int Id { get; set; }

    public Guid ExternalId { get; set; }
    [MaxLength(EntityConfigurationValues.DisplayNameMaximumLength)]
    [MinLength(EntityConfigurationValues.DisplayNameMinimumLength)]
    public string DisplayName { get; set; }

    [MaxLength(EntityConfigurationValues.DescriptionMaximumLength)]
    [MinLength(EntityConfigurationValues.DescriptionMinimumLength)]
    public string Description { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }
    public virtual Project Project { get; set; }
    public int ProjectId { get; set; }
}