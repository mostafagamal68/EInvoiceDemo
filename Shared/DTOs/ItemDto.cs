using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EInvoiceDemo.Shared.DTOs;

public class ItemDto
{
    [Required]
    public Guid ItemId { get; set; }

    [DisplayName("Item")]
    [Required]
    [StringLength(50)]
    public string? ItemName { get; set; }

    [DisplayName("Description")]
    [StringLength(150)]
    public string ItemDescription { get; set; } = string.Empty;

    [Required]
    public int ItemCode { get; set; }
}
