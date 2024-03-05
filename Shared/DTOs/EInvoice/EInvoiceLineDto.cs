using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Shared.DTOs;

public class EInvoiceLineDto : DtoBase, ICloneable
{
    [Key]
    [Required(ErrorMessage = "*")]
    public Guid? EInvoiceLineId { get; set; }

    [Required(ErrorMessage = "*")]
    public Guid? EInvoiceId { get; set; }

    [Required(ErrorMessage = "*")]
    public Guid? ItemId { get; set; }

    [Display(Name = "Item")]
    [Required(ErrorMessage = "*")]
    public string? ItemName { get; set; }

    [Display(Name = "Quantity")]
    [Required(ErrorMessage = "*")]
    [Range(0, int.MaxValue, ErrorMessage = "Min Value is 0")]
    public decimal? Quantity { get; set; }

    [Display(Name = "Amount Sold")]
    [Required(ErrorMessage = "*")]
    [Range(0, int.MaxValue, ErrorMessage = "Min Value is 0")]
    public decimal? AmountSold { get; set; }

    [Display(Name = "Total")]
    [Range(0, int.MaxValue, ErrorMessage = "Min Value is 0")]
    public decimal? Total { get; set; }

    [Display(Name = "Net Amount")]
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