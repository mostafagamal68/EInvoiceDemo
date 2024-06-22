using EInvoiceDemo.Server.Data;
using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Server.Services;
using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Helpers;
using EInvoiceDemo.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace EInvoiceDemo.Server.Repositories;

internal class ItemRepository(EInvoiceContext context)
    : ContextService(context), IItemRepository
{
    public async Task Add(Item model)
    {
        DbModel().Add(model);
        await _context.SaveChangesAsync();
    }

    public Task<string> Bulk(Bulk ids)
    {
        throw new NotImplementedException();
    }

    public DbSet<Item> DbModel() => _context.Items;
    
    public async Task Delete(Guid id)
    {
        var item = await DbModel().FindOrErrorAsync(id);
        if (_context.EInvoiceLines.Any(c => c.ItemId == id))
            throw new Exception("This Item used with E-Invoice line and cannot be deleted.");

        DbModel().Remove(item);
        await _context.SaveChangesAsync();
    }

    public bool Exists(Guid id) => DbModel().Any(e => e.ItemId == id);
    
    public async Task<int> GetCode() => (await DbModel().MaxAsync(c => (int?)c.ItemCode) ?? 0) + 1;
    
    public async Task<List<KeyValue>> GetKeyValue(string? filter)
        => await DbModel()
                .WhereIf(filter.HasValue(), c => (c.ItemCode + " " + c.ItemName).Contains(filter!))
                .Select(c => new KeyValue
                {
                    Key = c.ItemId,
                    Value = c.ItemCode + " - " + c.ItemName
                })
                .Take(30)
                .ToListAsync();

    public async Task<ItemsFilter> GetList(ItemsFilter? filter)
    {
        filter ??= new ItemsFilter();

        var query = Query()
                    .WhereIf(filter.ItemName.HasValue(), c => (c.ItemCode + " " + c.ItemName).Contains(filter.ItemName));

        filter.Pagination = Pagination.GetPagination<Item, ItemsFilter, ItemDto>(query, filter);

        filter.Items = await query
            .Select(c => new ItemDto
            {
                Id = c.ItemId,
                ItemName = c.ItemName,
                ItemCode = c.ItemCode,
                ItemDescription = c.ItemDescription,
            })
            .OrderWith(filter.SortField, filter.SortApproach)
            .Skip(filter.Pagination.PageNo * filter.Pagination.RowsCount)
            .Take(filter.Pagination.RowsCount)
            .ToListAsync();

        return filter;
    }

    public async Task<ItemDto> GetSingle(Guid id)
    {
        var item = await DbModel().FindOrErrorAsync(id);

        return new ItemDto
        {
            Id = item.ItemId,
            ItemName = item.ItemName,
            ItemCode = item.ItemCode,
            ItemDescription = item.ItemDescription,
        };
    }

    public IQueryable<Item> Query() => DbModel().AsQueryable();
    
    public async Task Update(ItemDto dto)
    {
        var Item = await DbModel().FindOrErrorAsync(dto.Id);

        Item.ItemName = dto.ItemName;
        Item.ItemCode = dto.ItemCode;
        Item.ItemDescription = dto.ItemDescription;

        _context.Entry(Item).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }
}
