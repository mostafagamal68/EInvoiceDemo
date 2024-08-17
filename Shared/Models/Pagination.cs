namespace EInvoiceDemo.Shared.Models;

public class Pagination
{
    public int PageNo { get; set; } = 0;
    public int CurrentPage { get; set; } = 0;
    public int RowsCount { get; set; } = 10;
    public int TotalRows { get; set; } = 0;
    public int PagesCount { get; set; } = 0;
    public int StartPage { get; set; } = 1;
    
    public static int GetFirstPage(string Page)
        => Page.Length == 1 ? 1 : int.Parse(Page.First().ToString()) + 9;
}

public enum SortingType
{
    Asc,
    Desc
}
