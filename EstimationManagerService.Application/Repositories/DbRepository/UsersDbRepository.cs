using System;
using EstimationManagerService.Application.Common.Exceptions;
using EstimationManagerService.Application.Repositories.Interfaces;
using EstimationManagerService.Persistance;
using Microsoft.EntityFrameworkCore;

namespace EstimationManagerService.Application.Repositories.DbRepository;


public class UsersDbRepository : IUsersDbRepository
{
    private readonly AppDbContext _dbContext;

    public UsersDbRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> GetUserIdByUserExternalIdAsync(Guid externalId, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.ExternalId == externalId, cancellationToken);

        if (user is null)
            throw new NotFoundException($"User with externalId: {externalId} not found");

        return user.Id;
    }
}
