namespace EInvoiceDemo.Shared.DTOs;

public class GlobalFilter<T>
{
    public List<T> Items { get; set; } = new();
    public Pagination Pagination { get; set; } = new();
}
