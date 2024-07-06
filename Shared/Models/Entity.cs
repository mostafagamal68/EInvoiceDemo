using System.ComponentModel.DataAnnotations;

namespace EInvoiceDemo.Shared.Models;

public class Entity
{
    [Key]
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}
