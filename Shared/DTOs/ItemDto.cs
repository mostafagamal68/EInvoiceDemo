using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EInvoiceDemo.Shared.DTOs;

public class ItemDto
{
    [Required]
    public decimal ItemId { get; set; }

    [Required]
    [StringLength(50)]
    public string? ItemName { get; set; }

    [StringLength(150)]
    public string ItemDescription { get; set; } = string.Empty;

    [Required]
    public int ItemCode { get; set; }
}
