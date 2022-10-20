using EstimationManagerService.Application.Common.Exceptions;
using EstimationManagerService.Application.Operations.Companies.Queries.Models;
using EstimationManagerService.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EstimationManagerService.Application.Operations.Companies.Queries.GetAllUserCompanies
{
    public class GetAllUserCompaniesQuery : IRequest<ICollection<UserCompanyDto>>
    {
        public Guid OnwerUserExternalId { get; set; }
    }

    public class GetAllUserCompaniesQueryHandler : IRequestHandler<GetAllUserCompaniesQuery, ICollection<UserCompanyDto>>
    {
        private readonly AppDbContext _dbContext;

        public GetAllUserCompaniesQueryHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<UserCompanyDto>> Handle(GetAllUserCompaniesQuery request, CancellationToken cancellationToken)
        {
            var userEntity = await _dbContext.Users.FirstOrDefaultAsync(x => x.ExternalId == request.OnwerUserExternalId, cancellationToken);

            if (userEntity is null)
                throw new NotFoundException("User", request.OnwerUserExternalId);

            var companies = await _dbContext.Companies.Where(x => x.AdminId == userEntity.Id).Select(x => new UserCompanyDto()
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