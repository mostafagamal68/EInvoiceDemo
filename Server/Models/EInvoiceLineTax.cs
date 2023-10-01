using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EInvoiceDemo.Server.Models;

public class EInvoiceLineTax
{
    [Key]
    public Guid EInvoiceLineTaxId { get; set; }
    
    public Guid TaxId { get; set; }
    public Tax Tax { get; set; }

    public Guid EInvoiceLineId { get; set; }

    [Column(TypeName = "decimal(28, 8)")]
    public decimal Amount { get; set; }
    public EInvoiceLine? EInvoiceLine { get; set; }

}
