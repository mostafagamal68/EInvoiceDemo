using EInvoiceDemo.Server.Data;
using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Server.Services;
using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Helpers;
using EInvoiceDemo.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace EInvoiceDemo.Server.Repositories;

internal class EInvoiceTypeRepository : ContextService, IEInvoiceTypeRepository
{
    public EInvoiceTypeRepository(EInvoiceContext context) : base(context) { }

    public async Task Add(EInvoiceType model)
    {
        DbModel().Add(model);
        await _context.SaveChangesAsync();
    }

    public Task<string> Bulk(Bulk ids)
    {
        throw new NotImplementedException();
    }

    public DbSet<EInvoiceType> DbModel() => _context.EInvoiceTypes;

    public async Task Delete(Guid id)
    {
        var eInvoiceType = await DbModel().FindOrErrorAsync(id);
        if (_context.EInvoices.Any(c => c.EInvoiceTypeId == id))
            throw new Exception("This type used with E-Invoice and cannot be deleted.");
        DbModel().Remove(eInvoiceType);
        await _context.SaveChangesAsync();
    }

    public bool Exists(Guid id) => DbModel().Any(e => e.EInvoiceTypeId == id);

    public Task<int> GetCode()
    {
        throw new NotImplementedException();
    }

    public async Task<List<KeyValue>> GetKeyValue(string? filter)
        => await DbModel()
                .WhereIf(filter.HasValue(), c => c.EInvoiceTypeName.Contains(filter!))
                .Select(c => new KeyValue
                {
                    Key = c.EInvoiceTypeId,
                    Value = c.EInvoiceTypeName
                })
                .Take(30)
                .ToListAsync();

    public async Task<EInvoiceTypesFilter> GetList(EInvoiceTypesFilter? filter)
    {
        filter ??= new EInvoiceTypesFilter();

        var query = Query()
            .WhereIf(filter.EInvoiceTypeName.HasValue(), c => c.EInvoiceTypeName.Contains(filter.EInvoiceTypeName!));

        filter.Pagination = Pagination.GetPagination<EInvoiceType, EInvoiceTypesFilter, EInvoiceTypeDto>(query, filter);

        filter.Items = await query
            .Select(c => new EInvoiceTypeDto
            {
                EInvoiceTypeId = c.EInvoiceTypeId,
                EInvoiceTypeName = c.EInvoiceTypeName,
            })
            .OrderWith(filter.SortField, filter.SortApproach)
            .Skip(filter.Pagination.PageNo * filter.Pagination.RowsCount)
            .Take(filter.Pagination.RowsCount)
            .ToListAsync();

        return filter;
    }

    public async Task<EInvoiceTypeDto> GetSingle(Guid id)
    {
        var eInvoiceType = await DbModel().FindOrErrorAsync(id);

        return new EInvoiceTypeDto
        {
            EInvoiceTypeId = eInvoiceType.EInvoiceTypeId,
            EInvoiceTypeName = eInvoiceType?.EInvoiceTypeName,
        };
    }

    public IQueryable<EInvoiceType> Query() => DbModel().AsQueryable();

    public async Task Update(EInvoiceTypeDto dto)
    {
        var eInvoiceType = await DbModel().FindOrErrorAsync(dto.EInvoiceTypeId);

        eInvoiceType.EInvoiceTypeName = dto.EInvoiceTypeName;

        _context.Entry(eInvoiceType).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}
