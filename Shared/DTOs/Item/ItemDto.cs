using EInvoiceDemo.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Shared.DTOs.Item;

public class ItemDto : DtoBase
{
    [Display(Name = "Code")]
    [Required(ErrorMessage = "*")]
    public int Code { get; set; }

    [Display(Name = "Item")]
    [Required]
    [StringLength(50)]
    public string? ItemName { get; set; }

    [Display(Name = "Description")]
    [StringLength(150)]
    public string ItemDescription { get; set; } = string.Empty;
}
