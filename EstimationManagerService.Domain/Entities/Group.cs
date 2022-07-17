using System;
using System.ComponentModel.DataAnnotations;
using EstimationManagerService.Domain.EntitiesConfigurations;

namespace EstimationManagerService.Domain.Entities
{
    public class Group
    {
        public int Id { get; set; }

        [MaxLength(EntityConfigurationValues.DisplayNameMaximumLenth)]
        [MinLength(EntityConfigurationValues.DisplayNameMinimumLenth)]
        public string DisplayName { get; set; }
        public virtual List<Project> Projects { get; set; }
        public virtual Company Company { get; set; }
        public int CompanyId { get; set; }

    }
}
