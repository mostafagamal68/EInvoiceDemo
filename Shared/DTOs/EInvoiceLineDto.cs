using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Shared.DTOs;

public class EInvoiceLineDto : ICloneable
{
    [Required(ErrorMessage = "*")]
    public Guid? EInvoiceLineId { get; set; }

    [Required(ErrorMessage = "*")]
    public Guid? EInvoiceId { get; set; }

    [Required(ErrorMessage = "*")]
    public Guid? ItemId { get; set; }

    [Required(ErrorMessage = "*")]
    public string? ItemName { get; set; }

    [Required(ErrorMessage = "*")]
    [Range(0, int.MaxValue, ErrorMessage = "Min Value is 0")]
    public decimal? Quantity { get; set; }

    [Required(ErrorMessage = "*")]
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