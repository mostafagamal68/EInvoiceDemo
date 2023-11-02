using System.ComponentModel;
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

    [DisplayName("Item")]
    [Required(ErrorMessage = "*")]
    public string? ItemName { get; set; }

    [DisplayName("Quantity")]
    [Required(ErrorMessage = "*")]
    [Range(0, int.MaxValue, ErrorMessage = "Min Value is 0")]
    public decimal? Quantity { get; set; }

    [DisplayName("Amount Sold")]
    [Required(ErrorMessage = "*")]
    [Range(0, int.MaxValue, ErrorMessage = "Min Value is 0")]
    public decimal? AmountSold { get; set; }

    [DisplayName("Total")]
    [Range(0, int.MaxValue, ErrorMessage = "Min Value is 0")]
    public decimal? Total { get; set; }

    [DisplayName("Net Amount")]
    [Range(0, int.MaxValue, ErrorMessage = "Min Value is 0")]
    public decimal? ItemNetAmount { get; set; }

    [ValidateComplexType] 
    public List<EInvoiceLineTaxDto> EInvoiceLineTaxes { get; set; } = new();
    public object Clone()
    {
        EInvoiceLineDto l = (EInvoiceLineDto)MemberwiseClone();

        List<EInvoiceLineTaxDto> copyObjectList = new();

        foreach (EInvoiceLineTaxDto o in l.EInvoiceLineTaxes)
        {
            EInvoiceLineTaxDto co = (EInvoiceLineTaxDto)o.Clone();
            copyObjectList.Add(co);
        }

        l.EInvoiceLineTaxes = copyObjectList;
        return l;
    }
}