using System;
using EstimationManagerService.Application.Operations.Companies.Queries.Models;
using EstimationManagerService.Application.Repositories.Interfaces;
using EstimationManagerService.Persistance;
using MediatR;

namespace EstimationManagerService.Application.Operations.Companies.Queries.GetAllUserCompanies
{
    public class GetAllUserCompaniesQuery : IRequest<ICollection<UserCompanyDto>>
    {
        public Guid OnwerUserId { get; set; }
    }

    public class GetAllUserCompaniesQueryHandler : IRequestHandler<GetAllUserCompaniesQuery, ICollection<UserCompanyDto>>
    {
        private readonly AppDbContext _dbContext;
        private readonly IUsersDbRepository _usersDbRepository;

        public GetAllUserCompaniesQueryHandler(AppDbContext dbContext, IUsersDbRepository usersDbRepository)
        {
            _dbContext = dbContext;
            _usersDbRepository = usersDbRepository;
        }
        public async Task<ICollection<UserCompanyDto>> Handle(GetAllUserCompaniesQuery request, CancellationToken cancellationToken)
        {
            var ownerUserId = await _usersDbRepository.GetUserIdByUserExternalIdAsync(request.OnwerUserId, cancellationToken);
            // var companies = await _dbContext.Companies.Where(x => x.AdminId == ownerUserId).Select(x => new )
            throw new NotImplementedException();
        }
    }
}
