using EstimationManagerService.Domain.Entities;

namespace EstimationManagerService.Application.Repositories.Interfaces;

public interface ICompaniesDbRepository
{
    Task<Company> GetOwnersCompany(int ownerUserId, Guid companyExternalId, CancellationToken cancellationToken = default);
}
