namespace EInvoiceDemo.Shared.Models;

public class Pagination
{
    public int PageNo { get; set; } = 0;
    public int CurrentPage { get; set; } = 0;
    public int RowsCount { get; set; } = 10;
    public int TotalRows { get; set; } = 0;
    public int PagesCount { get; set; } = 0;
    public int StartPage { get; set; } = 1;
    public static Pagination GetPagination<TModel, TFilter, TDto>(IQueryable<TModel> query, TFilter filter)
        where TModel : class
        where TDto : DtoBase
        where TFilter : GlobalFilter<TDto>
    {
        var count = query.Count();
        filter.Pagination.CurrentPage.ToString();
        while ((filter.Pagination.PageNo * filter.Pagination.RowsCount) > count) filter.Pagination.PageNo--;
        return new Pagination
        {
            PageNo = filter.Pagination.PageNo,
            CurrentPage = filter.Pagination.PageNo,
            RowsCount = filter.Pagination.RowsCount,
            TotalRows = count,
            StartPage = GetFirstPage((filter.Pagination.PageNo + 1).ToString()),
            PagesCount = (int)Math.Ceiling((decimal)count / filter.Pagination.RowsCount)
        };
    }
    private static int GetFirstPage(string Page)
        => Page.Length == 1 ? 1 : int.Parse(Page.First().ToString()) + 9;
}

public enum SortingType
{
    Asc,
    Desc
}
