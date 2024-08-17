namespace EInvoiceDemo.Shared.Models;

public class GlobalFilter<T> where T : DtoBase
{
    public List<T> Items { get; set; } = [];
    public Pagination Pagination { get; set; } = new();
    public string? SortField { get; set; }
    public SortingType SortApproach { get; set; } = SortingType.Desc;
}