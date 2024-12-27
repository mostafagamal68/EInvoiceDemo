using EInvoiceDemo.Shared.Models;

namespace EInvoiceDemo.Shared.DTOs.Tax;

public class TaxesFilter : GlobalFilter<TaxDto>
{
    public string? TaxName { get; set; }
}
