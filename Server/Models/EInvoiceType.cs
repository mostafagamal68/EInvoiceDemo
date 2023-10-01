using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EInvoiceDemo.Server.Models;

public class EInvoiceType
{
    public EInvoiceType()
    {
        EInvoices = new List<EInvoice>();
    }

    [Key]
    public Guid EInvoiceTypeId { get; set; }
    [Required]
    [StringLength(50)]
    public string? EInvoiceTypeName { get; set; }
    public IList<EInvoice> EInvoices { get; set; }
}
