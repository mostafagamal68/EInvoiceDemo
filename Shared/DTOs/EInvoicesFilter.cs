using EInvoiceDemo.Shared.Models;

namespace EInvoiceDemo.Shared.DTOs;

public class EInvoicesFilter : GlobalFilter<EInvoiceDto>
{
    public int EInvoiceCode { get; set; }

    public Guid? EInvoiceTypeId { get; set; }

    public Guid? CustomerId { get; set; }

    public Guid? ItemId { get; set; }

    public Guid? TaxId { get; set; }

    public DateTime? DateTimeIssued { get; set; }
}
