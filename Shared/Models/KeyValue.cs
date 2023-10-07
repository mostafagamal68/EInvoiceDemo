namespace EInvoiceDemo.Shared.Models;

public class KeyValue
{
    public Guid? Id { get; set; }
    public Guid? Key { get; set; }
    public string Value { get; set; } = string.Empty;
}
