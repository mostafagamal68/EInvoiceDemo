using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Shared.DTOs;

public class EInvoiceLineDto : ICloneable
{
    [Required]
    public Guid? EInvoiceLineId { get; set; }

    [Required]
    public Guid? EInvoiceId { get; set; }

    [Required]
    public Guid? ItemId { get; set; }

    [Required]
    public string? ItemName { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Min Value is 0")]
    public decimal? Quantity { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Min Value is 0")]
    public decimal? AmountSold { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Min Value is 0")]
    public decimal? Total { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Min Value is 0")]
    public decimal? ItemNetAmount { get; set; }

    [ValidateComplexType] public List<EInvoiceLineTaxDto> EInvoiceLineTaxes { get; set; } = new();
    public object Clone()
    {
        return (EInvoiceLineDto)MemberwiseClone();
    }
}