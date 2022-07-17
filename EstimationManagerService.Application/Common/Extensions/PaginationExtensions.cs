using System;
using EstimationManagerService.Application.Common.Helpers;
using Microsoft.EntityFrameworkCore;

namespace EstimationManagerService.Application.Common.Extensions;

public static class PaginationExtensions
{
    public static async Task<PageModel<T>> PaginateAsync<T>(
        this IQueryable<T> query,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var pageModel = new PageModel<T>();

        pageModel.CurrentPage = page <= 0 ? 1 : page;
        pageModel.PageSize = pageSize;
        pageModel.TotalItems = await query.CountAsync(cancellationToken);

        var startRow = (page - 1) * pageSize;
        pageModel.Data = await query
            .Skip(startRow)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        pageModel.TotalPages = (int)Math.Ceiling(pageModel.TotalItems / (double)pageSize);

        return pageModel;
    }
}
