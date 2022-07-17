using System;

namespace EstimationManagerService.Application.Repositories.Interfaces;

public interface IUsersDbRepository
{
    Task<int> GetUserIdByUserExternalIdAsync(Guid externalId, CancellationToken cancellationToken = default);
}
