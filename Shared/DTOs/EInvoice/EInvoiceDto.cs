﻿using EInvoiceDemo.Shared.Enums;
using EInvoiceDemo.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Shared.DTOs.EInvoice;

public class EInvoiceDto : DtoBase
{
    [Display(Name = "Code")]
    [Required(ErrorMessage = "*")]
    public int Code { get; set; }

    [Display(Name = "Type")]
    [Required(ErrorMessage = "*")]
    public EInvoiceTypeEnum? EInvoiceType { get; set; }

    [Required(ErrorMessage = "*")]
    public Guid? CustomerId { get; set; }

    [Display(Name = "Customer")]
    [Required(ErrorMessage = "*")]
    public string? CustomerName { get; set; }

    [Required(ErrorMessage = "*")]
    public DateTime? DateTimeIssued { get; set; } = DateTime.Now;

    [Display(Name = "Net Amount")]
    public decimal NetAmount { get; set; }

    public List<EInvoiceLineDto> EInvoiceLines { get; set; } = [];
}
