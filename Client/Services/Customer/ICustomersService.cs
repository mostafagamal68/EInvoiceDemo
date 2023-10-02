using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Models;

namespace EInvoiceDemo.Client.Services;

public interface ICustomersService
{
    Task<CustomersFilter?> GetList(CustomersFilter? filter);
    Task<List<KeyValue>?> GetKeyValue(string? filter);
    Task<CustomerDto> GetSingle(Guid? Id);
    Task<int> GetCode();
    Task<HttpResponseMessage> Create(CustomerDto dto);
    Task<HttpResponseMessage> Edit(CustomerDto dto);
    Task<HttpResponseMessage> Delete(Guid? Id);
}
