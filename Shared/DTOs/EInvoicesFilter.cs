using EInvoiceDemo.Shared.Models;

namespace EInvoiceDemo.Shared.DTOs;

public class EInvoicesFilter : GlobalFilter<EInvoiceDto>
{
    public int EInvoiceCode { get; set; }

    public Guid? EInvoiceTypeId { get; set; }
    public string? EInvoiceTypeName { get; set; }

    public Guid? CustomerId { get; set; }
    public string? CustomerName { get; set; }

    public DateTime? DateTimeIssuedFrom { get; set; }
    public DateTime? DateTimeIssuedTo { get; set; }
}
