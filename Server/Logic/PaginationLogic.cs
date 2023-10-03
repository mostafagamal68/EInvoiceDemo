using EInvoiceDemo.Shared.Models;

namespace EInvoiceDemo.Server.Logic;

public class PaginationLogic<M,D>
{
    public static Pagination GetPagination<T>(IQueryable<M> query, T filter) where T : GlobalFilter<D>
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
