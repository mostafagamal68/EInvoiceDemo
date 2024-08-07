﻿using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Shared.Models;

public class DtoBase
{
    [Key]
    [Required]
    public Guid Id { get; set; }

    public DateTime CreationDate { get; set; }
    public DateTime ModifiedDate { get; set; }

    public bool IsSelected { get; set; }
    public bool IsDisabled { get; set; }
}
