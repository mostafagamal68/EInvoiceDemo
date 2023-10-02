using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Models;

namespace EInvoiceDemo.Client.Services;

public interface ITaxesService
{
    Task<TaxesFilter?> GetList(TaxesFilter? filter);
    Task<List<KeyValue>?> GetKeyValue(string? filter);
    Task<TaxDto> GetSingle(Guid? Id);
    Task<int> GetCode(); 
    Task<HttpResponseMessage> Create(TaxDto dto);
    Task<HttpResponseMessage> Edit(TaxDto dto);
    Task<HttpResponseMessage> Delete(Guid? Id);
}
