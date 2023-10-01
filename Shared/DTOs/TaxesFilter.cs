namespace EInvoiceDemo.Shared.DTOs;

public class TaxesFilter : GlobalFilter<TaxDto>
{
    public string? TaxName { get; set; }
}
