using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Shared.DTOs;

public class EInvoiceLineTaxDto
{
    [Required]
    public Guid EInvoiceLineTaxId { get; set; }

    [Required]
    public Guid? EInvoiceLineId { get; set; }

    [Required]
    public Guid? TaxId { get; set; }

    [Required]
    public string? TaxName { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Min Value is 0")]
    public decimal? Amount { get; set; }
}