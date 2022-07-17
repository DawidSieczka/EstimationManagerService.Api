using EstimationManagerService.Domain.EntitiesConfigurations;
using System.ComponentModel.DataAnnotations;

namespace EstimationManagerService.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public Guid ExternalId { get; set; }

    [MaxLength(EntityConfigurationValues.DisplayNameMaximumLenth)]
    [MinLength(EntityConfigurationValues.DisplayNameMinimumLenth)]
    public string DisplayName { get; set; }
    public virtual List<UserTask> UserTasks { get; set; }
    public virtual List<Company> Companies { get; set; }
    public virtual List<Company> OwnCompanies { get; set; }

}