using EInvoiceDemo.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Server.Models;

public class Item : Entity
{
    public Item()
    {
        EInvoiceLines = [];
    }

    [Required]
    public int Code { get; set; }

    [Required]
    [StringLength(50)]
    public string? ItemName { get; set; }

    [StringLength(150)]
    public string ItemDescription { get; set; } = string.Empty;

    public IList<EInvoiceLine> EInvoiceLines { get; set; }

}
