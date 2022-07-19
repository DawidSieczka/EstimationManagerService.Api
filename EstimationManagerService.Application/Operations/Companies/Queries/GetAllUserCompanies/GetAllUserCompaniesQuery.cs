using EstimationManagerService.Application.Operations.Companies.Queries.Models;
using EstimationManagerService.Application.Repositories.Interfaces;
using EstimationManagerService.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var companies = await _dbContext.Companies.Where(x => x.AdminId == ownerUserId).Select(x => new UserCompanyDto()
            {
                ExternalId = x.ExternalId,
                DisplayName = x.DisplayName,
                Groups = x.Groups.Select(y => new GroupsDto()
                {
                    DisplayName = y.DisplayName,
                    ExternalId = y.ExternalId
                }).ToList(),
                Users = x.Users.Select(y => new UserDto()
                {
                    DisplayName = y.DisplayName,
                    ExternalId = y.ExternalId
                }).ToList()
            }).ToListAsync(cancellationToken);

            return companies;
        }
    }
}