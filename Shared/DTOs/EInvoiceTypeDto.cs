using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EInvoiceDemo.Shared.DTOs;

public class EInvoiceTypeDto
{
    [Required]
    public Guid EInvoiceTypeId { get; set; }
    [DisplayName("Type")]
    [Required]
    [StringLength(50)]
    public string? EInvoiceTypeName { get; set; }
}
