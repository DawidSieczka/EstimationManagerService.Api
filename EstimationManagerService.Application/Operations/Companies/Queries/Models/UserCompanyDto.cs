using System;

namespace EstimationManagerService.Application.Operations.Companies.Queries.Models;

public class UserCompanyDto
{
    public Guid ExternalId { get; set; }
    public string DisplayName { get; set; }
    public List<UserDto> Users { get; set; }
    public List<GroupsDto> Groups { get; set; }


}

public class UserDto
{
    public Guid ExternalId { get; set; }

    public string DisplayName { get; set; }
}

public class GroupsDto
{
    public Guid ExternalId { get; set; }
    public string DisplayName { get; set; }

}