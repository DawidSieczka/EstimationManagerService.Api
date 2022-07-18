using EstimationManagerService.Application.Repositories.Interfaces;
using EstimationManagerService.Persistance;
using MediatR;

namespace EstimationManagerService.Application.Operations.Companies.Commands.DeleteCompany;

public class DeleteCompanyCommand : IRequest
{
    public Guid OwnerUserExternalId { get; set; }
    public Guid CompanyExternalId { get; set; }
}

public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
{
    private readonly AppDbContext _dbContext;
    private readonly IUsersDbRepository _usersDbRepository;
    private readonly ICompaniesDbRepository _companiesDbRepository;

    public DeleteCompanyCommandHandler(AppDbContext dbContext,
                                        IUsersDbRepository usersDbRepository,
                                        ICompaniesDbRepository companiesDbRepository)
    {
        _dbContext = dbContext;
        _usersDbRepository = usersDbRepository;
        _companiesDbRepository = companiesDbRepository;
    }
    public async Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        var ownerUserId = await _usersDbRepository.GetUserIdByUserExternalIdAsync(request.OwnerUserExternalId, cancellationToken);
        var companyEntity = await _companiesDbRepository.GetOwnersCompany(ownerUserId, request.CompanyExternalId, cancellationToken);

        _dbContext.Remove(companyEntity);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
