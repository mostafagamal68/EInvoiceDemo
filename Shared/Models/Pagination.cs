namespace EInvoiceDemo.Shared.Models;

public class Pagination
{
    public int PageNo { get; set; } = 0;
    public int CurrentPage { get; set; } = 0;
    public int RowsCount { get; set; } = 10;
    public int TotalRows { get; set; } = 0;
    public int PagesCount { get; set; } = 0;
    public static Pagination GetPagination<TModel, TFilter, TDto>(IQueryable<TModel> query, TFilter filter) where TFilter : GlobalFilter<TDto>
    {
        var count = query.Count();
        while ((filter.Pagination.PageNo * filter.Pagination.RowsCount) > count) filter.Pagination.PageNo--;
        return new Pagination
        {
            PageNo = filter.Pagination.PageNo,
            CurrentPage = filter.Pagination.PageNo,
            RowsCount = filter.Pagination.RowsCount,
            TotalRows = count,
            PagesCount = (int)Math.Ceiling((decimal)count / filter.Pagination.RowsCount)
        };
    }
}
