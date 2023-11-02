using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace EInvoiceDemo.Shared.DTOs;

public class EInvoiceLineTaxDto : ICloneable
{
    [Required(ErrorMessage = "*")]
    public Guid EInvoiceLineTaxId { get; set; }

    [Required(ErrorMessage = "*")]
    public Guid? EInvoiceLineId { get; set; }

    [Required(ErrorMessage = "*")]
    public Guid? TaxId { get; set; }

    [Required(ErrorMessage = "*")]
    public string? TaxName { get; set; }

    [Required(ErrorMessage = "*")]
    [Range(0, int.MaxValue, ErrorMessage = "Min Value is 0")]
    public decimal? Amount { get; set; }

    public object Clone()
        => (EInvoiceLineTaxDto)MemberwiseClone();
}