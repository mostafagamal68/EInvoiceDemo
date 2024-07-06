using EInvoiceDemo.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Server.Models;

public class Tax : Entity
{
    public Tax()
    {
        EInvoiceLineTaxes = [];
    }

    [Required]
    public int Code { get; set; }

    [Required]
    [StringLength(50)]
    public string? TaxName { get; set; }

    [StringLength(150)]
    public string TaxDescription { get; set; } = string.Empty;

    public IList<EInvoiceLineTax> EInvoiceLineTaxes { get; set; }

}
