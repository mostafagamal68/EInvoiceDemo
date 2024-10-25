using EInvoiceDemo.Shared.Enums;
using EInvoiceDemo.Shared.Models;

namespace EInvoiceDemo.Client.Services;

public interface IGenericService<TFilter, TDto>
    where TDto : DtoBase
    where TFilter : GlobalFilter<TDto>
{
    string Api { get; }
    Task<TFilter> GetList(TFilter filter);
    Task<List<KeyValue>> GetKeyValue(string? filter);
    Task<TDto> GetSingle(Guid? Id);
    Task<int> GetCode();
    Task<HttpResponseMessage> Create(TDto dto);
    Task<HttpResponseMessage> Edit(TDto dto);
    Task<HttpResponseMessage> Delete(Guid? Id);
    Task<HttpResponseMessage> Bulk(List<Guid> Ids, BulkOperation operation);
}
