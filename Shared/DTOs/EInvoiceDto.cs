using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Shared.DTOs;

public class EInvoiceDto
{
    [Key]
    [Required(ErrorMessage = "*")]
    public Guid EInvoiceId { get; set; }

    [Display(Name = "Code")]
    [Required(ErrorMessage = "*")]
    public int EInvoiceCode { get; set; }

    [Required(ErrorMessage = "*")]
    public Guid? EInvoiceTypeId { get; set; }

    [Display(Name = "Type")]
    [Required(ErrorMessage = "*")]
    public string? EInvoiceTypeName { get; set; }

    [Required(ErrorMessage = "*")]
    public Guid? CustomerId { get; set; }

    [Display(Name = "Customer")]
    [Required(ErrorMessage = "*")]
    public string? CustomerName { get; set; }

    [Required(ErrorMessage = "*")]
    public DateTime? DateTimeIssued { get; set; } = DateTime.Now;

    [Display(Name = "Net Amount")]
    public decimal NetAmount { get; set; }

    public List<EInvoiceLineDto> EInvoiceLines { get; set; } = new();
}
