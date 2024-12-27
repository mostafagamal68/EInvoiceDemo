using EInvoiceDemo.Shared.Models;

namespace EInvoiceDemo.Shared.DTOs.Item;

public class ItemsFilter : GlobalFilter<ItemDto>
{
    public string? ItemName { get; set; }
}
