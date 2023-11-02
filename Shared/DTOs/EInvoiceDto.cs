using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Shared.DTOs;

public class EInvoiceDto
{
    [Key]
    [Required(ErrorMessage = "*")]
    public Guid EInvoiceId { get; set; }

    [DisplayName("Code")]
    [Required(ErrorMessage = "*")]
    public int EInvoiceCode { get; set; }

    [Required(ErrorMessage = "*")]
    public Guid? EInvoiceTypeId { get; set; }

    [DisplayName("Type")]
    [Required(ErrorMessage = "*")]
    public string? EInvoiceTypeName { get; set; }

    [Required(ErrorMessage = "*")]
    public Guid? CustomerId { get; set; }

    [DisplayName("Customer")]
    [Required(ErrorMessage = "*")]
    public string? CustomerName { get; set; }

    [Required(ErrorMessage = "*")]
    public DateTime? DateTimeIssued { get; set; } = DateTime.Now;

    [DisplayName("Net Amount")]
    public decimal NetAmount { get; set; }

    public List<EInvoiceLineDto> EInvoiceLines { get; set; } = new();
}
