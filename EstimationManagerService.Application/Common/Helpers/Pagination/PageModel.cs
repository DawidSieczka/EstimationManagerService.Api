using System;

namespace EstimationManagerService.Application.Common.Helpers;

public class PageModel<T>
{
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    private int _pageSize { get; set; }
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > 0 ? value : 1;
    }

    public int CurrentPage { get; set; }

    public IList<T> Data { get; set; } = new List<T>();
}
