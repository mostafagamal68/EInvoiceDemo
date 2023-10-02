using EInvoiceDemo.Shared.DTOs;

namespace EInvoiceDemo.Client.Services;

public interface IEInvoicesService
{
    Task<EInvoicesFilter?> GetList(EInvoicesFilter? filter);
    Task<EInvoiceDto> GetSingle(Guid? Id);
    Task<int> GetCode();
    Task<HttpResponseMessage> Create(EInvoiceDto dto);
    Task<HttpResponseMessage> Edit(EInvoiceDto dto);
    Task<HttpResponseMessage> Delete(Guid? Id);
}
