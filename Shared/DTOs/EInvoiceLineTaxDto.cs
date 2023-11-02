using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Shared.DTOs;

public class EInvoiceLineTaxDto : ICloneable
{
    [Key]
    [Required(ErrorMessage = "*")]
    public Guid EInvoiceLineTaxId { get; set; }

    [Required(ErrorMessage = "*")]
    public Guid? EInvoiceLineId { get; set; }

    [Required(ErrorMessage = "*")]
    public Guid? TaxId { get; set; }

    [Display(Name = "Tax")]
    [Required(ErrorMessage = "*")]
    public string? TaxName { get; set; }

    [Display(Name = "Amount")]
    [Required(ErrorMessage = "*")]
    [Range(0, int.MaxValue, ErrorMessage = "Min Value is 0")]
    public decimal? Amount { get; set; }

    public object Clone()
        => (EInvoiceLineTaxDto)MemberwiseClone();
}