using EInvoiceDemo.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Server.Models;

public class Customer : Entity
{
    public Customer()
    {
        EInvoices = [];
    }

    [Required]
    public int Code { get; set; }

    [Required]
    [StringLength(50)]
    public string? CustomerName { get; set; }

    public IList<EInvoice> EInvoices { get; set; }

}
