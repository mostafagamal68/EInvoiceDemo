using EInvoiceDemo.Shared.Enums;
using EInvoiceDemo.Shared.Models;

namespace EInvoiceDemo.Shared.DTOs;

public class EInvoicesFilter : GlobalFilter<EInvoiceDto>
{
    public int EInvoiceCode { get; set; }

    public EInvoiceTypeEnum? EInvoiceType { get; set; }

    public Guid? CustomerId { get; set; }
    public string? CustomerName { get; set; }

    public DateTime? DateTimeIssuedFrom { get; set; }
    public DateTime? DateTimeIssuedTo { get; set; }
}
