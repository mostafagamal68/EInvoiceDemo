﻿using EInvoiceDemo.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Shared.DTOs.Tax;

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
