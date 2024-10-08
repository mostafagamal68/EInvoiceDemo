﻿using EInvoiceDemo.Shared.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EInvoiceDemo.Server.Models;

public class EInvoiceLine : Entity
{
    public EInvoiceLine()
    {
        EInvoiceLineTaxes = [];
    }

    public Guid ItemId { get; set; }
    public Item Item { get; set; }

    public Guid EInvoiceId { get; set; }
    public EInvoice EInvoice { get; set; }

    [Column(TypeName = "decimal(28, 8)")]
    public decimal Quantity { get; set; }

    [Column(TypeName = "decimal(28, 8)")]
    public decimal AmountSold { get; set; }

    [Column(TypeName = "decimal(28, 8)")]
    public decimal Total { get; set; }

    [Column(TypeName = "decimal(28, 8)")]
    public decimal ItemNetAmount { get; set; }

    public IList<EInvoiceLineTax> EInvoiceLineTaxes { get; set; }
}
