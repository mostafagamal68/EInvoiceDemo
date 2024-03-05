using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Shared.DTOs;

namespace EInvoiceDemo.Server.Repositories;

public interface IItemRepository : IGenericRepository<Item, ItemDto, ItemsFilter>
{
}
