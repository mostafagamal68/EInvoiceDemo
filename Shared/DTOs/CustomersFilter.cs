using EInvoiceDemo.Shared.Models;

namespace EInvoiceDemo.Shared.DTOs;

public class CustomersFilter : GlobalFilter<CustomerDto>
{
    public string? CustomerName { get; set; }
}
