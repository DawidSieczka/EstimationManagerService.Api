using System;
using EstimationManagerService.Application.Common.Exceptions;
using EstimationManagerService.Domain.Entities;
using EstimationManagerService.Persistance;
using Microsoft.EntityFrameworkCore;
using EstimationManagerService.Application.Repositories.Interfaces;
namespace EstimationManagerService.Application.Repositories.DbRepository;

public class CompaniesDbRepository : ICompaniesDbRepository
{
    private readonly AppDbContext _dbContext;

    public CompaniesDbRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Company> GetOwnersCompany(int ownerUserId, Guid companyExternalId, CancellationToken cancellationToken = default)
    {
        var company = await _dbContext.Companies.FirstOrDefaultAsync(x => x.AdminId == ownerUserId && x.ExternalId == companyExternalId, cancellationToken);
        if (company is null)
            throw new NotFoundException($"Company with id: {companyExternalId} not found");

        return company;
    }
}
