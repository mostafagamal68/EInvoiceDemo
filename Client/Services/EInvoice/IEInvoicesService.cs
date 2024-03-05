using EInvoiceDemo.Shared.DTOs;

namespace EInvoiceDemo.Client.Services;

internal interface IEInvoicesService : IBaseService<EInvoicesFilter, EInvoiceDto>
{
    Task<HttpResponseMessage> BulkDelete(List<Guid> Ids);
}
