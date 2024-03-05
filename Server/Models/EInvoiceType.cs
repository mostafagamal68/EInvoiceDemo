using System.ComponentModel.DataAnnotations;

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
    public string EInvoiceTypeName { get; set; } = string.Empty;
    public IList<EInvoice> EInvoices { get; set; }
}
