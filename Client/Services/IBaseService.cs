using EInvoiceDemo.Shared.Models;

namespace EInvoiceDemo.Client.Services;

internal interface IBaseService<TFilter, TDTO>
{
    public Task<TFilter?> GetList(TFilter? filter);
    public Task<List<KeyValue>?> GetKeyValue(string? filter);
    public Task<TDTO> GetSingle(Guid? Id);
    public Task<int> GetCode();
    public Task<HttpResponseMessage> Create(TDTO dto);
    public Task<HttpResponseMessage> Edit(TDTO dto);
    public Task<HttpResponseMessage> Delete(Guid? Id);
}
