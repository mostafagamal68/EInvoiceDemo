using EInvoiceDemo.Shared.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EInvoiceDemo.Server.Models;

public class EInvoiceLineTax : Entity
{    
    public Guid TaxId { get; set; }
    public Tax Tax { get; set; }

    public Guid EInvoiceLineId { get; set; }

    [Column(TypeName = "decimal(28, 8)")]
    public decimal Amount { get; set; }
    public EInvoiceLine? EInvoiceLine { get; set; }

}
