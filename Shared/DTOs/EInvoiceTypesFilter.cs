using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EInvoiceDemo.Shared.Models;

namespace EInvoiceDemo.Shared.DTOs;

public class EInvoiceTypesFilter : GlobalFilter<EInvoiceTypeDto>
{
    public string? EInvoiceTypeName { get; set; }
}
