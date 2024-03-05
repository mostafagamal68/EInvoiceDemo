using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Shared.DTOs;

public class TaxDto : DtoBase
{
    [Key]
    [Required]
    public Guid TaxId { get; set; }

    [Display(Name = "Tax")]
    [Required]
    [StringLength(50)]
    public string? TaxName { get; set; }

    [Display(Name = "Description")]
    [StringLength(150)]
    public string TaxDescription { get; set; } = string.Empty;

    [Required]
    public int TaxCode { get; set; }
}
