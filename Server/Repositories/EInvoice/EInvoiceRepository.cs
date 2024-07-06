using EInvoiceDemo.Server.Data;
using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Server.Services;
using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Helpers;
using EInvoiceDemo.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EInvoiceDemo.Server.Repositories;

internal class EInvoiceRepository(EInvoiceContext context)
    : ContextService(context), IEInvoiceRepository
{
    public EInvoice AddLogic(EInvoiceDto dto)
    {
        EInvoice eInvoice = new()
        {
            Id = dto.Id,
            CustomerId = dto.CustomerId.GetValueOrDefault(),
            Code = dto.Code,
            DateTimeIssued = dto.DateTimeIssued.GetValueOrDefault(),
            EInvoiceType = dto.EInvoiceType.GetValueOrDefault(),
            NetAmount = dto.NetAmount,
        };
        foreach (var line in dto.EInvoiceLines)
        {
            foreach (var item in line.EInvoiceLineTaxes.GroupBy(x => x.TaxId).Select(x => new { x.Key, Taxes = x }))
            {
                if (item.Taxes.Count() > 1) throw new Exception($"Item {line.ItemName} with Net Amount {line.ItemNetAmount} has duplicated taxes.");
            }
            EInvoiceLine eInvoiceLine = new()
            {
                Id = line.Id,
                EInvoiceId = line.EInvoiceId.GetValueOrDefault(),
                AmountSold = line.AmountSold.GetValueOrDefault(),
                ItemId = line.ItemId.GetValueOrDefault(),
                Quantity = line.Quantity.GetValueOrDefault(),
                Total = line.Total.GetValueOrDefault(),
                ItemNetAmount = line.ItemNetAmount.GetValueOrDefault(),
            };
            foreach (var tax in line.EInvoiceLineTaxes)
            {
                EInvoiceLineTax eInvoiceLineTax = new()
                {
                    Id = tax.Id,
                    EInvoiceLineId = tax.EInvoiceLineId.GetValueOrDefault(),
                    TaxId = tax.TaxId.GetValueOrDefault(),
                    Amount = tax.Amount.GetValueOrDefault(),
                };
                eInvoiceLine.EInvoiceLineTaxes.Add(eInvoiceLineTax);
            }
            eInvoice.EInvoiceLines.Add(eInvoiceLine);
        }
        return eInvoice;
    }

    public async Task Add(EInvoice model)
    {
        DbModel().Add(model);
        await _context.SaveChangesAsync();
    }

    public async Task<string> Bulk(Bulk ids)
    {
        await Query().Where(c => ids.Guids.Contains(c.Id)).ExecuteDeleteAsync();
        return "Deleted Successfully";
    }

    public DbSet<EInvoice> DbModel() => _context.EInvoices;

    public async Task Delete(Guid id)
    {
        var eInvoice = await Query().FirstOrErrorAsync(c => c.Id == id);
        DbModel().Remove(eInvoice);
        await _context.SaveChangesAsync();
    }

    public bool Exists(Guid id) => DbModel().Any(e => e.Id == id);

    public async Task<int> GetCode() => (await DbModel().MaxAsync(c => (int?)c.Code) ?? 0) + 1;

    public Task<List<KeyValue>> GetKeyValue(string? filter)
    {
        throw new NotImplementedException();
    }

    public async Task<EInvoicesFilter> GetList(EInvoicesFilter? filter)
    {
        filter ??= new EInvoicesFilter();

        var query = Query()
                    .WhereIf(filter.CustomerId.HasValue, c => c.CustomerId == filter.CustomerId)
                    .WhereIf(filter.EInvoiceType.HasValue, c => c.EInvoiceType == filter.EInvoiceType)
                    .WhereIf(filter.DateTimeIssuedFrom.HasValue, c => c.DateTimeIssued >= filter.DateTimeIssuedFrom)
                    .WhereIf(filter.DateTimeIssuedTo.HasValue, c => c.DateTimeIssued <= filter.DateTimeIssuedTo);

        filter.Pagination = Pagination.GetPagination<EInvoice, EInvoicesFilter, EInvoiceDto>(query, filter);

        filter.Items = await query
            .Select(c => new EInvoiceDto
            {
                Id = c.Id,
                CustomerId = c.CustomerId,
                CustomerName = c.Customer.CustomerName,
                EInvoiceType = c.EInvoiceType,
                DateTimeIssued = c.DateTimeIssued,
                Code = c.Code,
                NetAmount = c.NetAmount,
            })
            .OrderWith(filter.SortField, filter.SortApproach)
            .Skip(filter.Pagination.PageNo * filter.Pagination.RowsCount)
            .Take(filter.Pagination.RowsCount)
            .ToListAsync();

        return filter;
    }

    public async Task<EInvoiceDto> GetSingle(Guid id)
    {
        var eInvoice = await Query().FirstOrErrorAsync(c => c.Id == id);

        return new EInvoiceDto
        {
            Id = eInvoice.Id,
            CustomerId = eInvoice.CustomerId,
            CustomerName = eInvoice.Customer?.CustomerName,
            EInvoiceType = eInvoice.EInvoiceType,
            DateTimeIssued = eInvoice.DateTimeIssued,
            Code = eInvoice.Code,
            NetAmount = eInvoice.NetAmount,
            EInvoiceLines = eInvoice.EInvoiceLines.Select(c => new EInvoiceLineDto
            {
                Id = c.Id,
                EInvoiceId = c.EInvoiceId,
                ItemId = c.ItemId,
                ItemName = c.Item?.ItemName,
                AmountSold = c.AmountSold,
                Quantity = c.Quantity,
                Total = c.Total,
                ItemNetAmount = c.ItemNetAmount,
                EInvoiceLineTaxes = c.EInvoiceLineTaxes.Select(x => new EInvoiceLineTaxDto
                {
                    Id = x.Id,
                    EInvoiceLineId = x.EInvoiceLineId,
                    TaxId = x.TaxId,
                    TaxName = x.Tax.TaxName,
                    Amount = x.Amount
                })
                .ToList()
            })
            .ToList()
        };
    }

    public IQueryable<EInvoice> Query()
    => _context.EInvoices
            .Include(c => c.Customer)
            .Include(c => c.EInvoiceLines).ThenInclude(c => c.EInvoiceLineTaxes).ThenInclude(c => c.Tax)
            .Include(c => c.EInvoiceLines).ThenInclude(c => c.Item)
            .AsQueryable();

    public async Task Update(EInvoiceDto dto)
    {
        var eInvoice = await Query().FirstOrErrorAsync(c => c.Id == dto.Id);

        _context.Entry(eInvoice).State = EntityState.Modified;

        eInvoice.Id = dto.Id;
        eInvoice.CustomerId = dto.CustomerId.GetValueOrDefault();
        eInvoice.Code = dto.Code;
        eInvoice.DateTimeIssued = dto.DateTimeIssued.GetValueOrDefault();
        eInvoice.EInvoiceType = dto.EInvoiceType.GetValueOrDefault();
        eInvoice.NetAmount = dto.NetAmount;

        foreach (var line in dto.EInvoiceLines)
        {
            foreach (var item in line.EInvoiceLineTaxes.GroupBy(x => x.TaxId).Select(x => new { x.Key, Taxes = x }))
            {
                if (item.Taxes.Count() > 1) throw new Exception($"Item {line.ItemName} with Net Amount {line.ItemNetAmount} has duplicated taxes.");
            }
            var eInvoiceLine = eInvoice.EInvoiceLines.FirstOrDefault(c => c.Id == line.Id);
            if (eInvoiceLine is null)
            {
                eInvoiceLine = new()
                {
                    Id = line.Id,
                    EInvoiceId = line.EInvoiceId.GetValueOrDefault(),
                    AmountSold = line.AmountSold.GetValueOrDefault(),
                    ItemId = line.ItemId.GetValueOrDefault(),
                    Quantity = line.Quantity.GetValueOrDefault(),
                    Total = line.Total.GetValueOrDefault(),
                    ItemNetAmount = line.ItemNetAmount.GetValueOrDefault(),
                };
                eInvoice.EInvoiceLines.Add(eInvoiceLine);
                _context.Entry(eInvoiceLine).State = EntityState.Added;
                foreach (var tax in line.EInvoiceLineTaxes)
                {
                    EInvoiceLineTax eInvoiceLineTax = new()
                    {
                        Id = tax.Id,
                        EInvoiceLineId = tax.EInvoiceLineId.GetValueOrDefault(),
                        TaxId = tax.TaxId.GetValueOrDefault(),
                        Amount = tax.Amount.GetValueOrDefault(),
                    };
                    eInvoiceLine.EInvoiceLineTaxes.Add(eInvoiceLineTax);
                    _context.Entry(eInvoiceLineTax).State = EntityState.Added;
                }
            }
            else
            {
                _context.Entry(eInvoiceLine).State = EntityState.Modified;

                eInvoiceLine.Id = line.Id;
                eInvoiceLine.EInvoiceId = line.EInvoiceId.GetValueOrDefault();
                eInvoiceLine.AmountSold = line.AmountSold.GetValueOrDefault();
                eInvoiceLine.ItemId = line.ItemId.GetValueOrDefault();
                eInvoiceLine.Quantity = line.Quantity.GetValueOrDefault();
                eInvoiceLine.Total = line.Total.GetValueOrDefault();
                eInvoiceLine.ItemNetAmount = line.ItemNetAmount.GetValueOrDefault();
                foreach (var tax in line.EInvoiceLineTaxes)
                {
                    var eInvoiceLineTax = eInvoiceLine.EInvoiceLineTaxes.FirstOrDefault(c => c.Id == tax.Id);
                    if (eInvoiceLineTax is null)
                    {
                        eInvoiceLineTax = new()
                        {
                            Id = tax.Id,
                            EInvoiceLineId = tax.EInvoiceLineId.GetValueOrDefault(),
                            TaxId = tax.TaxId.GetValueOrDefault(),
                            Amount = tax.Amount.GetValueOrDefault(),
                        };
                        eInvoiceLine.EInvoiceLineTaxes.Add(eInvoiceLineTax);
                        _context.Entry(eInvoiceLineTax).State = EntityState.Added;
                    }
                    else
                    {
                        _context.Entry(eInvoiceLineTax).State = EntityState.Modified;

                        eInvoiceLineTax.Id = tax.Id;
                        eInvoiceLineTax.EInvoiceLineId = tax.EInvoiceLineId.GetValueOrDefault();
                        eInvoiceLineTax.TaxId = tax.TaxId.GetValueOrDefault();
                        eInvoiceLineTax.Amount = tax.Amount.GetValueOrDefault();
                    }
                }
                var deletedLineTaxes = eInvoiceLine.EInvoiceLineTaxes.Where(c => !line.EInvoiceLineTaxes.Any(x => c.Id == x.Id));
                if (deletedLineTaxes.Any())
                {
                    foreach (var item in deletedLineTaxes)
                    {
                        _context.Entry(item).State = EntityState.Deleted;
                        _context.EInvoiceLineTaxes.Remove(item);
                    }
                }
            }
        }
        var deletedLines = eInvoice.EInvoiceLines.Where(c => !dto.EInvoiceLines.Any(x => c.Id == x.Id)).ToList();
        if (deletedLines.Any())
        {
            foreach (var item in deletedLines)
            {
                _context.Entry(item).State = EntityState.Deleted;
                _context.EInvoiceLines.Remove(item);
            }
        }
        await _context.SaveChangesAsync();
    }

    public async Task<List<KeyValue>> GetKeyValue(string? filter, Expression<Func<EInvoice, bool>>? filterPredicate = null)
    {
        return await GetKeyValue(filter);
    }

    public async Task<EInvoicesFilter> GetList(EInvoicesFilter? filter, Func<IQueryable<EInvoice>>? queryable = null)
    {
        return await GetList(filter);
    }
}
