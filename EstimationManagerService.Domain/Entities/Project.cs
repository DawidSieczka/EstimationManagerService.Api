using System;
using System.ComponentModel.DataAnnotations;
using EstimationManagerService.Domain.EntitiesConfigurations;

namespace EstimationManagerService.Domain.Entities;

public class Project
{
    public int Id { get; set; }

    [MaxLength(EntityConfigurationValues.DisplayNameMaximumLenth)]
    [MinLength(EntityConfigurationValues.DisplayNameMinimumLenth)]
    public string DisplayName { get; set; }
    public virtual List<UserTask> Tasks { get; set; }
    public virtual Group Group { get; set; }
    public int GroupId { get; set; }
}
