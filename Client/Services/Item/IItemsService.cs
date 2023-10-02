using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Models;

namespace EInvoiceDemo.Client.Services;

public interface IItemsService
{
    Task<ItemsFilter?> GetList(ItemsFilter? filter);
    Task<List<KeyValue>?> GetKeyValue(string? filter);
    Task<ItemDto> GetSingle(Guid? Id);
    Task<int> GetCode(); 
    Task<HttpResponseMessage> Create(ItemDto dto);
    Task<HttpResponseMessage> Edit(ItemDto dto);
    Task<HttpResponseMessage> Delete(Guid? Id);
}
