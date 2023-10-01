using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Server.Models;

public class Tax
{
    public Tax()
    {
        EInvoiceLineTaxes = new List<EInvoiceLineTax>();
    }

    [Key]
    public Guid TaxId { get; set; }

    [Required]
    [StringLength(50)]
    public string? TaxName { get; set; }

    [StringLength(150)]
    public string TaxDescription { get; set; } = string.Empty;

    [Required]
    public int TaxCode { get; set; }
    public IList<EInvoiceLineTax> EInvoiceLineTaxes { get; set; }

}
