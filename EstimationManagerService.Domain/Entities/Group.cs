using System;
using System.ComponentModel.DataAnnotations;
using EstimationManagerService.Domain.EntitiesConfigurations;

namespace EstimationManagerService.Domain.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public Guid ExternalId { get; set; }
        [MaxLength(EntityConfigurationValues.DisplayNameMaximumLength)]
        [MinLength(EntityConfigurationValues.DisplayNameMinimumLength)]
        public string DisplayName { get; set; }
        public virtual List<Project> Projects { get; set; }
        public virtual Company Company { get; set; }
        public int CompanyId { get; set; }

    }
}
