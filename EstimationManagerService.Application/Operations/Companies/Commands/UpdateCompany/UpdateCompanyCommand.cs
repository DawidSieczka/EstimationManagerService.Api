using System;
using EstimationManagerService.Application.Common.Exceptions;
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

    public UpdateCompanyCommandHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var userEntity = await _dbContext.Users.FirstOrDefaultAsync(x => x.ExternalId == request.OwnerUserExternalId, cancellationToken);

        if (userEntity is null)
            throw new NotFoundException("User", request.OwnerUserExternalId);

        var companyEntity =
            await _dbContext.Companies.FirstOrDefaultAsync(
                x => x.AdminId == userEntity.Id && x.ExternalId == request.CompanyExternalId, cancellationToken);
        
        if (companyEntity is null)
            throw new NotFoundException("Company", request.CompanyExternalId);

        companyEntity.DisplayName = request.DisplayName;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
