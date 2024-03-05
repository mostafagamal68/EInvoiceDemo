using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Shared.DTOs;

public class EInvoiceTypeDto : DtoBase
{
    [Key]
    [Required]
    public Guid EInvoiceTypeId { get; set; }
    [Display(Name = "Type")]
    [Required]
    [StringLength(50)]
    public string? EInvoiceTypeName { get; set; }
}
