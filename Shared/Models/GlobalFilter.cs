using EInvoiceDemo.Shared.Enums;

namespace EInvoiceDemo.Shared.Models;

public class GlobalFilter<T> where T : DtoBase
{
    public List<T> Items { get; set; } = [];
    public Pagination Pagination { get; set; } = new();
    public string? SortField { get; set; }
    public SortingType SortApproach { get; set; } = SortingType.Desc;

    public void GetPagination<TEntity>(IQueryable<TEntity> query)
        where TEntity : Entity
    {
        int count = query.Count();
        while ((Pagination.PageNo * Pagination.RowsCount) > count) Pagination.PageNo--;
        Pagination = new Pagination
        {
            PageNo = Pagination.PageNo,
            CurrentPage = Pagination.PageNo,
            RowsCount = Pagination.RowsCount,
            PagesCount = (int)Math.Ceiling((decimal)count / Pagination.RowsCount)
        };
    }
}