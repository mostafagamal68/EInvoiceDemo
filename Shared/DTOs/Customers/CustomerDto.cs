using System.ComponentModel.DataAnnotations;
using EInvoiceDemo.Shared.Models;

namespace EInvoiceDemo.Shared.DTOs;

public class CustomerDto : DtoBase
{
    [Display(Name = "Customer")]
    [Required]
    [StringLength(50)]
    public string? CustomerName { get; set; }

    [Display(Name = "Code")]
    [Required]
    public int CustomerCode { get; set; }
}
