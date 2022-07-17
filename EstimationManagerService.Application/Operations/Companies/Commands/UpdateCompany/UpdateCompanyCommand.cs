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

    public UpdateCompanyCommandHandler(AppDbContext dbContext, IUsersDbRepository usersDbRepository)
    {
        _dbContext = dbContext;
        _usersDbRepository = usersDbRepository;
    }

    public async Task<Unit> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var ownerUserId = await _usersDbRepository.GetUserIdByUserExternalIdAsync(request.OwnerUserExternalId, cancellationToken);

        var companyEntity = await _dbContext.Companies.FirstOrDefaultAsync(x => x.ExternalId == request.CompanyExternalId && x.AdminId == ownerUserId, cancellationToken);

        if (companyEntity is null)
            throw new NotFoundException($"Company with id: {request.CompanyExternalId} for admin with id: {request.OwnerUserExternalId} not found");

        companyEntity.DisplayName = request.DisplayName;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
