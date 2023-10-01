namespace EInvoiceDemo.Shared.DTOs;

public class EInvoiceTypesFilter : GlobalFilter<EInvoiceTypeDto>
{
    public string? EInvoiceTypeName { get; set; }
}
