using EstimationManagerService.Application.Common.Exceptions;
using EstimationManagerService.Application.Operations.Groups.Queries.Models;
using EstimationManagerService.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EstimationManagerService.Application.Operations.Groups.Queries.GetGroups;

public class GetGroupsQuery : IRequest<IEnumerable<GroupDto>>
{
    public Guid CompanyExternalId { get; set; }
}

public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, IEnumerable<GroupDto>>
{
    private readonly AppDbContext _dbContext;

    public GetGroupsQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<GroupDto>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
    {
        var companyEntity = await _dbContext.Companies.FirstOrDefaultAsync(x => x.ExternalId == request.CompanyExternalId,
                cancellationToken);

        if (companyEntity is null) throw new NotFoundException("Company", request.CompanyExternalId);

        var groups = companyEntity.Groups.Select(x => new GroupDto()
        {
            DisplayName = x.DisplayName,
            ExternalId = x.ExternalId
        });

        return groups;
    }
}