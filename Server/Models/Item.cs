﻿using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Server.Models;

public class Item
{
    public Item()
    {
        EInvoiceLines = new List<EInvoiceLine>();
    }

    [Key]
    public Guid ItemId { get; set; }

    [Required]
    [StringLength(50)]
    public string? ItemName { get; set; }

    [StringLength(150)]
    public string ItemDescription { get; set; } = string.Empty;

    [Required]
    public int ItemCode { get; set; }
    public IList<EInvoiceLine> EInvoiceLines { get; set; }

}
