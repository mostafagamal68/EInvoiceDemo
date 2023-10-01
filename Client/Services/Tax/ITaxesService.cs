using EInvoiceDemo.Shared.DTOs;

namespace EInvoiceDemo.Client.Services;

public interface ITaxesService
{
    Task<TaxesFilter?> GetList(TaxesFilter? filter);
    Task<TaxDto> GetSingle(Guid? Id);
    Task<HttpResponseMessage> Create(TaxDto dto);
    Task<HttpResponseMessage> Edit(TaxDto dto);
    Task<HttpResponseMessage> Delete(Guid? Id);
}
