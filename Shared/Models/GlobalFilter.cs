namespace EInvoiceDemo.Shared.Models;

public class GlobalFilter<T>
{
    public List<T> Items { get; set; } = new();
    public Pagination Pagination { get; set; } = new();
    public string? SortField { get; set; }
    public bool SortApproach { get; set; } = true;
}
