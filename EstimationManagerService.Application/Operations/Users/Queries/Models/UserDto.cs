using System;

namespace EstimationManagerService.Application.Operations.Users.Queries.Models;

public class UserDto
{
    public Guid ExternalId { get; set; }

    public string DisplayName { get; set; }

}
