using EInvoiceDemo.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Shared.DTOs.Customers;

public class CustomerDto : DtoBase
{
    [Display(Name = "Code")]
    [Required(ErrorMessage = "*")]
    public int Code { get; set; }

    [Display(Name = "Customer")]
    [Required]
    [StringLength(50)]
    public string? CustomerName { get; set; }
}
