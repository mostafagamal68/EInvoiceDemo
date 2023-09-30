using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Server.Models;

public class EInvoiceLineTax
{
    [Key]
    public decimal EInvoiceLineTaxId { get; set; }
    
    public decimal TaxId { get; set; }
    public Tax Tax { get; set; }

    public decimal EInvoiceLineId { get; set; }

    public EInvoiceLine? EInvoiceLine { get; set; }

}
