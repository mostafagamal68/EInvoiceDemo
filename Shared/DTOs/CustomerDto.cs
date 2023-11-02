using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Shared.DTOs;

public class CustomerDto
{
    public Guid CustomerId { get; set; }

    [DisplayName("Customer")]
    [Required]
    [StringLength(50)]
    public string? CustomerName { get; set; }

    [DisplayName("Code")]
    [Required]
    public int CustomerCode { get; set; }
}
