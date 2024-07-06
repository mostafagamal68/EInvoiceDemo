using System.ComponentModel.DataAnnotations;
using EInvoiceDemo.Shared.Models;

namespace EInvoiceDemo.Shared.DTOs;

public class TaxDto : DtoBase
{
    [Display(Name = "Code")]
    [Required(ErrorMessage = "*")]
    public int Code { get; set; }

    [Display(Name = "Tax")]
    [Required]
    [StringLength(50)]
    public string? TaxName { get; set; }

    [Display(Name = "Description")]
    [StringLength(150)]
    public string TaxDescription { get; set; } = string.Empty;
}
