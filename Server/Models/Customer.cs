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
    [Column(TypeName = "decimal(28, 8)")]
    public decimal CustomerId { get; set; }

    [Required]
    [StringLength(50)]
    public string? CustomerName { get; set; }

    [Required]
    public int CustomerCode { get; set; }
    public IList<EInvoice> EInvoices { get; set; }

}
