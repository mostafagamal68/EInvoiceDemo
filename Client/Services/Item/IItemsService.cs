using EInvoiceDemo.Shared.DTOs;

namespace EInvoiceDemo.Client.Services;

public interface IItemsService
{
    Task<ItemsFilter?> GetList(ItemsFilter? filter);
    Task<ItemDto> GetSingle(Guid? Id);
    Task<HttpResponseMessage> Create(ItemDto dto);
    Task<HttpResponseMessage> Edit(ItemDto dto);
    Task<HttpResponseMessage> Delete(Guid? Id);
}
