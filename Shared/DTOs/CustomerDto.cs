using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Shared.DTOs;

public class CustomerDto
{
    [Key]
    [Required]
    public Guid CustomerId { get; set; }

    [Display(Name = "Customer")]
    [Required]
    [StringLength(50)]
    public string? CustomerName { get; set; }

    [Display(Name = "Code")]
    [Required]
    public int CustomerCode { get; set; }
}
