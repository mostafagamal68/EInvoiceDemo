namespace EInvoiceDemo.Shared.Models;

public class Bulk
{
    public List<Guid>? Guids { get; set; }
    public BulkOperation BulkOperation { get; set; }
}
public enum BulkOperation
{
    Update,
    Delete,
}
