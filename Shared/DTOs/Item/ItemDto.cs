﻿using System.ComponentModel.DataAnnotations;
using EInvoiceDemo.Shared.Models;

namespace EInvoiceDemo.Shared.DTOs;

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
