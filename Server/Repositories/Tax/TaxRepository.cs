using EInvoiceDemo.Server.Data;
using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Server.Services;
using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Helpers;
using EInvoiceDemo.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace EInvoiceDemo.Server.Repositories;

internal class TaxRepository : ContextService, ITaxRepository
{
    public TaxRepository(EInvoiceContext context) : base(context) { }

    public async Task Add(Tax model)
    {
        DbModel().Add(model);
        await _context.SaveChangesAsync();
    }

    public Task<string> Bulk(Bulk ids)
    {
        throw new NotImplementedException();
    }

    public DbSet<Tax> DbModel() => _context.Taxes;
    
    public async Task Delete(Guid id)
    {
        var tax = await DbModel().FindOrErrorAsync(id);
        if (_context.EInvoiceLineTaxes.Any(c => c.EInvoiceLineTaxId == id))
            throw new Exception("This Tax used with E-Invoice line taxes and cannot be deleted.");
        DbModel().Remove(tax);
        await _context.SaveChangesAsync();
    }

    public bool Exists(Guid id) => DbModel().Any(e => e.TaxId == id);
    
    public async Task<int> GetCode() => (await Query().MaxAsync(c => (int?)c.TaxCode) ?? 0) + 1;
    
    public async Task<List<KeyValue>> GetKeyValue(string? filter)
        => await DbModel()
                .WhereIf(filter.HasValue(), c => (c.TaxCode + " " + c.TaxName).Contains(filter!))
                .Select(c => new KeyValue
                {
                    Key = c.TaxId,
                    Value = c.TaxCode + " - " + c.TaxName
                })
                .Take(30)
                .ToListAsync();
    
    public async Task<TaxesFilter> GetList(TaxesFilter? filter)
    {
        filter ??= new TaxesFilter();

        var query = Query()
                    .WhereIf(filter.TaxName.HasValue(), c => (c.TaxCode + " " + c.TaxName).Contains(filter.TaxName!));

        filter.Pagination = Pagination.GetPagination<Tax, TaxesFilter, TaxDto>(query, filter);

        filter.Items = await query
            .Select(c => new TaxDto
            {
                TaxId = c.TaxId,
                TaxName = c.TaxName,
                TaxCode = c.TaxCode,
                TaxDescription = c.TaxDescription,
            })
            .OrderWith(filter.SortField, filter.SortApproach)
            .Skip(filter.Pagination.PageNo * filter.Pagination.RowsCount)
            .Take(filter.Pagination.RowsCount)
            .ToListAsync();

        return filter;
    }

    public async Task<TaxDto> GetSingle(Guid id)
    {
        var tax = await DbModel().FindOrErrorAsync(id);

        return new TaxDto
        {
            TaxId = tax.TaxId,
            TaxName = tax.TaxName,
            TaxCode = tax.TaxCode,
            TaxDescription = tax.TaxDescription,
        };
    }

    public IQueryable<Tax> Query() => DbModel().AsQueryable();
    
    public async Task Update(TaxDto dto)
    {
        var Tax = await DbModel().FindOrErrorAsync(dto.TaxId);

        Tax.TaxName = dto.TaxName;
        Tax.TaxCode = dto.TaxCode;
        Tax.TaxDescription = dto.TaxDescription;

        _context.Entry(Tax).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }
}
