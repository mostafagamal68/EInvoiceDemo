using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EInvoiceDemo.Server.Models;

public class Customer
{
    public Customer()
    {
        EInvoices = new List<EInvoice>();
    }

    [Key]
    public Guid CustomerId { get; set; }

    [Required]
    [StringLength(50)]
    public string? CustomerName { get; set; }

    [Required]
    public int CustomerCode { get; set; }
    public IList<EInvoice> EInvoices { get; set; }

}
