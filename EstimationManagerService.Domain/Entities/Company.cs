using System;
using System.ComponentModel.DataAnnotations;
using EstimationManagerService.Domain.EntitiesConfigurations;

namespace EstimationManagerService.Domain.Entities;

public class Company
{
    public int Id { get; set; }

    [MaxLength(EntityConfigurationValues.DisplayNameMaximumLenth)]
    [MinLength(EntityConfigurationValues.DisplayNameMinimumLenth)]
    public string DisplayName { get; set; }
    public virtual User Admin { get; set; }
    public int AdminId { get; set; }
    public virtual List<User> Users { get; set; }
    public virtual List<Group> Groups { get; set; }
}
