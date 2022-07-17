using System;
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
        private readonly IUsersDbRepository _usersDbRepository;

        public CreateCompanyCommandHandler(AppDbContext dbContext, IGuidHelper guidHelper, IUsersDbRepository usersDbRepository)
        {
            _dbContext = dbContext;
            _guidHelper = guidHelper;
            _usersDbRepository = usersDbRepository;
        }

        public async Task<Guid> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var ownerUserId = await _usersDbRepository.GetUserIdByUserExternalIdAsync(request.OwnerUserExternalId, cancellationToken);

            var companyEntity = await _dbContext.Companies.AddAsync(new Company()
            {
                DisplayName = request.DisplayName,
                ExternalId = _guidHelper.CreateGuid(),
                AdminId = ownerUserId
            }, cancellationToken);

            return companyEntity.Entity.ExternalId;
        }
    }
}
