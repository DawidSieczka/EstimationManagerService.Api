using EstimationManagerService.Application.Common.Exceptions;
using EstimationManagerService.Application.Common.Helpers.MockingHelpers.Interfaces;
using EstimationManagerService.Application.Repositories.Interfaces;
using EstimationManagerService.Domain.Entities;
using EstimationManagerService.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EstimationManagerService.Application.Operations.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommand : IRequest<Guid>
    {
        public Guid OwnerUserExternalId { get; set; }
        public string DisplayName { get; set; }
    }

    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Guid>
    {
        private readonly AppDbContext _dbContext;
        private readonly IGuidHelper _guidHelper;

        public CreateCompanyCommandHandler(AppDbContext dbContext, IGuidHelper guidHelper)
        {
            _dbContext = dbContext;
            _guidHelper = guidHelper;
        }

        public async Task<Guid> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var ownerUserEntity = await _dbContext.Users.FirstOrDefaultAsync(x => x.ExternalId == request.OwnerUserExternalId, cancellationToken);
            if (ownerUserEntity is null)
                throw new NotFoundException($"User with externalId: {request.OwnerUserExternalId} not found");

            var companyEntity = await _dbContext.Companies.AddAsync(new Company()
            {
                DisplayName = request.DisplayName,
                ExternalId = _guidHelper.CreateGuid(),
                AdminId = ownerUserEntity.Id,
                Users = new List<User>() { ownerUserEntity }
            }, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return companyEntity.Entity.ExternalId;
        }
    }
}