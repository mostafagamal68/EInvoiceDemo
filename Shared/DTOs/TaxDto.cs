using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Shared.DTOs;

public class TaxDto
{
    public Guid TaxId { get; set; }

    [DisplayName("Tax")]
    [Required]
    [StringLength(50)]
    public string? TaxName { get; set; }

    [DisplayName("Description")]
    [StringLength(150)]
    public string TaxDescription { get; set; } = string.Empty;

    [Required]
    public int TaxCode { get; set; }
}
