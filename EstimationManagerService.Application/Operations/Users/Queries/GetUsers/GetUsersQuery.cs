using System;
using EstimationManagerService.Application.Common.Extensions;
using EstimationManagerService.Application.Common.Helpers;
using EstimationManagerService.Application.Operations.Users.Queries.Models;
using EstimationManagerService.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EstimationManagerService.Application.Operations.Users.Queries.GetUsers;

public class GetUsersQuery : IRequest<PageModel<UserDto>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PageModel<UserDto>>
{
    private readonly AppDbContext _dbContext;

    public GetUsersQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<PageModel<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var paginatedUsersEntities = await _dbContext.Users
            .AsNoTracking()
            .PaginateAsync(request.Page, request.PageSize, cancellationToken);

        return new PageModel<UserDto>()
        {
            PageSize = paginatedUsersEntities.PageSize,
            CurrentPage = paginatedUsersEntities.CurrentPage,
            TotalItems = paginatedUsersEntities.TotalItems,
            TotalPages = paginatedUsersEntities.TotalPages,
            Data = paginatedUsersEntities.Data.Select(x => new UserDto()
            {
                DisplayName = x.DisplayName,
                ExternalId = x.ExternalId
            }).ToList()
        };
    }
}
