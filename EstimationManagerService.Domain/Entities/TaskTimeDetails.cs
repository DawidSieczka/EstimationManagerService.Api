using System.ComponentModel.DataAnnotations;
using EstimationManagerService.Domain.EntitiesConfigurations;

namespace EstimationManagerService.Domain.Entities;

public class TaskTimeDetails
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    [MaxLength(EntityConfigurationValues.DescriptionMaximumLength)]
    [MinLength(EntityConfigurationValues.DescriptionMinimumLength)]
    public string Description { get; set; }
    
    
    public virtual UserTask UserTask { get; set; }
    public int UserTaskId { get; set; }
}