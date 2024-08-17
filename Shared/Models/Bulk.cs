using EInvoiceDemo.Shared.Enums;

namespace EInvoiceDemo.Shared.Models;

public class Bulk
{
    public List<Guid>? Guids { get; set; }
    public BulkOperation BulkOperation { get; set; }
}
