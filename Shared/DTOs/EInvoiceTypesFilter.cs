using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EInvoiceDemo.Shared.Models;

namespace EInvoiceDemo.Shared.DTOs;

public class EInvoiceTypesFilter
{
    public string? EInvoiceTypeName { get; set; }

    public List<EInvoiceTypeDto> Items { get; set; } = new();
    public Pagination Pagination { get; set; } = new();
}
