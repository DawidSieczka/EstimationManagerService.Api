using System;
using EstimationManagerService.Application.Common.Exceptions;
using EstimationManagerService.Application.Repositories.Interfaces;
using EstimationManagerService.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EstimationManagerService.Application.Operations.Companies.Commands.UpdateCompany;

public class UpdateCompanyCommand : IRequest
{
    public Guid OwnerUserExternalId { get; set; }
    public Guid CompanyExternalId { get; set; }
    public string DisplayName { get; set; }
}

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand>
{
    private readonly AppDbContext _dbContext;
    private readonly IUsersDbRepository _usersDbRepository;
    private readonly ICompaniesDbRepository _companiesDbRepository;

    public UpdateCompanyCommandHandler(AppDbContext dbContext, IUsersDbRepository usersDbRepository, ICompaniesDbRepository companiesDbRepository)
    {
        _dbContext = dbContext;
        _usersDbRepository = usersDbRepository;
        _companiesDbRepository = companiesDbRepository;
    }

    public async Task<Unit> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var ownerUserId = await _usersDbRepository.GetUserIdByUserExternalIdAsync(request.OwnerUserExternalId, cancellationToken);
        var companyEntity = await _companiesDbRepository.GetOwnersCompany(ownerUserId, request.CompanyExternalId, cancellationToken);
        
        companyEntity.DisplayName = request.DisplayName;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
