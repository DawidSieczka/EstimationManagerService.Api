using EstimationManagerService.Application.Common.Exceptions;
using EstimationManagerService.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EstimationManagerService.Application.Operations.Companies.Commands.DeleteCompany;

public class DeleteCompanyCommand : IRequest
{
    public Guid OwnerUserExternalId { get; set; }
    public Guid CompanyExternalId { get; set; }
}

public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
{
    private readonly AppDbContext _dbContext;

    public DeleteCompanyCommandHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.ExternalId == request.OwnerUserExternalId, cancellationToken);

        if (user is null)
            throw new NotFoundException("User", request.OwnerUserExternalId);

        var companyEntity =
            await _dbContext.Companies.FirstOrDefaultAsync(
                x => x.AdminId == user.Id && x.ExternalId == request.CompanyExternalId, cancellationToken);
        
        if (companyEntity is null)
            throw new NotFoundException("Company", request.CompanyExternalId);

        _dbContext.Remove(companyEntity);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
