using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Shared.DTOs;

public class EInvoiceDto
{
    [Required(ErrorMessage = "*")]
    public Guid EInvoiceId { get; set; }

    [Required(ErrorMessage = "*")]
    public int EInvoiceCode { get; set; }

    [Required(ErrorMessage = "*")]
    public Guid? EInvoiceTypeId { get; set; }

    [Required(ErrorMessage = "*")]
    public string? EInvoiceTypeName { get; set; }

    [Required(ErrorMessage = "*")]
    public Guid? CustomerId { get; set; }

    [Required(ErrorMessage = "*")]
    public string? CustomerName { get; set; }

    [Required(ErrorMessage = "*")]
    public DateTime? DateTimeIssued { get; set; } = DateTime.Now;

    public decimal NetAmount { get; set; }

    public List<EInvoiceLineDto> EInvoiceLines { get; set; } = new();
}
