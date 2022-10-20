using EstimationManagerService.Domain.EntitiesConfigurations;
using System.ComponentModel.DataAnnotations;

namespace EstimationManagerService.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public Guid ExternalId { get; set; }

    [MaxLength(EntityConfigurationValues.DisplayNameMaximumLength)]
    [MinLength(EntityConfigurationValues.DisplayNameMinimumLength)]
    public string DisplayName { get; set; }
    public virtual List<UserTask> UserTasks { get; set; }
    public virtual List<Company> Companies { get; set; }
    public virtual List<Company> OwnCompanies { get; set; }

}