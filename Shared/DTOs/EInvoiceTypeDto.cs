using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EInvoiceDemo.Shared.DTOs;

public class EInvoiceTypeDto
{
    [Required]
    public Guid EInvoiceTypeId { get; set; }
    [Required]
    [StringLength(50)]
    public string? EInvoiceTypeName { get; set; }
}
