using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Shared.DTOs;

public class CustomerDto
{
    public Guid CustomerId { get; set; }

    [Required]
    [StringLength(50)]
    public string? CustomerName { get; set; }

    [Required]
    public int CustomerCode { get; set; }
}
