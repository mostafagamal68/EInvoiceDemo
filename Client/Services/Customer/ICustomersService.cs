using EInvoiceDemo.Shared.DTOs;

namespace EInvoiceDemo.Client.Services;

public interface ICustomersService
{
    Task<CustomersFilter?> GetList(CustomersFilter? filter);
    Task<CustomerDto> GetSingle(Guid? Id);
    Task<HttpResponseMessage> Create(CustomerDto dto);
    Task<HttpResponseMessage> Edit(CustomerDto dto);
    Task<HttpResponseMessage> Delete(Guid? Id);
}
