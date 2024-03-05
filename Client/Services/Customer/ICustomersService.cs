using EInvoiceDemo.Shared.DTOs;

namespace EInvoiceDemo.Client.Services;

internal interface ICustomersService : IBaseService<CustomersFilter, CustomerDto>
{
    
}
