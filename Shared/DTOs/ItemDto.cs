using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Shared.DTOs;

public class ItemDto
{
    [Key]
    [Required]
    public Guid ItemId { get; set; }

    [Display(Name = "Item")]
    [Required]
    [StringLength(50)]
    public string? ItemName { get; set; }

    [Display(Name = "Description")]
    [StringLength(150)]
    public string ItemDescription { get; set; } = string.Empty;

    [Required]
    public int ItemCode { get; set; }
}
