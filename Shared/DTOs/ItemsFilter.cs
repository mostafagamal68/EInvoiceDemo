namespace EInvoiceDemo.Shared.DTOs;

public class ItemsFilter : GlobalFilter<ItemDto>
{
    public string? ItemName { get; set; }
}
