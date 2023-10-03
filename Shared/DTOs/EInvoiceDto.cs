using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Shared.DTOs;

public class EInvoiceDto
{
    [Required]
    public Guid EInvoiceId { get; set; }

    [Required]
    public int EInvoiceCode { get; set; }

    [Required]
    public Guid? EInvoiceTypeId { get; set; }

    [Required]
    public string? EInvoiceTypeName { get; set; }

    [Required]
    public Guid? CustomerId { get; set; }

    [Required]
    public string? CustomerName { get; set; }

    [Required]
    public DateTime DateTimeIssued { get; set; } = DateTime.Now;

    public decimal NetAmount { get; set; }

    public List<EInvoiceLineDto> EInvoiceLines { get; set; } = new();
}
