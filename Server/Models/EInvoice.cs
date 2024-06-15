using EInvoiceDemo.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EInvoiceDemo.Server.Models;

public class EInvoice
{
    public EInvoice()
    {
        EInvoiceLines = new List<EInvoiceLine>();
    }

    [Key]
    public Guid EInvoiceId { get; set; }

    [Required]
    public int EInvoiceCode { get; set; }

    [Required]
    public EInvoiceTypeEnum? EInvoiceType { get; set;}

    [Required]
    public Guid CustomerId { get; set;}

    public Customer Customer { get; set;}

    [Required]
    public DateTime DateTimeIssued { get; set; }

    [Column(TypeName = "decimal(28, 8)")]
    public decimal NetAmount { get; set;}

    public IList<EInvoiceLine> EInvoiceLines { get; set; }
}
