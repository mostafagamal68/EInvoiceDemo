using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EInvoiceDemo.Server.Data;
using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Models;
using EInvoiceDemo.Server.Logic;

namespace EInvoiceDemo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EInvoicesController : ControllerBase
    {
        private readonly EInvoiceContext _context;

        public EInvoicesController(EInvoiceContext context)
        {
            _context = context;
        }

        private IQueryable<EInvoice> Query()
            => _context.EInvoices.Include(c => c.Customer).Include(c => c.EInvoiceType)
            .Include(c => c.EInvoiceLines).ThenInclude(c => c.EInvoiceLineTaxes).ThenInclude(c => c.Tax)
            .Include(c => c.EInvoiceLines).ThenInclude(c => c.Item)
            .AsQueryable();


        // GET: api/EInvoices/Code
        [HttpGet("Code")]
        public async Task<ActionResult<int>> GetEInvoiceCode()
            => (await _context.EInvoices?.MaxAsync(c => (int?)c.EInvoiceCode) ?? 0) + 1;

        // GET: api/EInvoices
        [HttpPost("{filter}")]
        public async Task<ActionResult<EInvoicesFilter>> GetEInvoices(EInvoicesFilter? filter)
        {
            var query = Query();
            if (filter is null) filter = new EInvoicesFilter();
            else
            {
                if (filter.CustomerId.HasValue)
                    query = query.Where(c => c.CustomerId == filter.CustomerId);
                if (filter.EInvoiceTypeId.HasValue)
                    query = query.Where(c => c.EInvoiceTypeId == filter.EInvoiceTypeId);
                if (filter.DateTimeIssuedFrom.HasValue)
                    query = query.Where(c => c.DateTimeIssued >= filter.DateTimeIssuedFrom);
                if (filter.DateTimeIssuedTo.HasValue)
                    query = query.Where(c => c.DateTimeIssued <= filter.DateTimeIssuedTo);
            }

            filter.Pagination = PaginationLogic<EInvoice, EInvoiceDto>.GetPagination(query, filter);

            filter.Items = await query
                .Select(c => new EInvoiceDto
                {
                    EInvoiceId = c.EInvoiceId,
                    CustomerId = c.CustomerId,
                    CustomerName = c.Customer.CustomerName,
                    EInvoiceTypeId = c.EInvoiceTypeId,
                    EInvoiceTypeName = c.EInvoiceType.EInvoiceTypeName,
                    DateTimeIssued = c.DateTimeIssued,
                    EInvoiceCode = c.EInvoiceCode,
                    NetAmount = c.NetAmount,
                })
                .OrderWith(filter.SortField, filter.SortApproach)
                .Skip(filter.Pagination.PageNo * filter.Pagination.RowsCount)
                .Take(filter.Pagination.RowsCount)
                .ToListAsync();

            return filter;
        }

        // GET: api/EInvoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EInvoiceDto>> GetEInvoice(Guid id)
        {
            var eInvoice = await Query().FirstOrDefaultAsync(c => c.EInvoiceId == id);

            if (eInvoice == null) return NotFound();

            return new EInvoiceDto
            {
                EInvoiceId = eInvoice.EInvoiceId,
                CustomerId = eInvoice.CustomerId,
                CustomerName = eInvoice.Customer.CustomerName,
                EInvoiceTypeId = eInvoice.EInvoiceTypeId,
                EInvoiceTypeName = eInvoice.EInvoiceType.EInvoiceTypeName,
                DateTimeIssued = eInvoice.DateTimeIssued,
                EInvoiceCode = eInvoice.EInvoiceCode,
                NetAmount = eInvoice.NetAmount,
                EInvoiceLines = eInvoice.EInvoiceLines.Select(c => new EInvoiceLineDto
                {
                    EInvoiceLineId = c.EInvoiceLineId,
                    EInvoiceId = c.EInvoiceId,
                    ItemId = c.ItemId,
                    ItemName = c.Item.ItemName,
                    AmountSold = c.AmountSold,
                    Quantity = c.Quantity,
                    Total = c.Total,
                    ItemNetAmount = c.ItemNetAmount,
                    EInvoiceLineTaxes = c.EInvoiceLineTaxes.Select(x => new EInvoiceLineTaxDto
                    {
                        EInvoiceLineTaxId = x.EInvoiceLineTaxId,
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

        // PUT: api/EInvoices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutEInvoice(EInvoiceDto dto)
        {
            var eInvoice = await Query().FirstOrDefaultAsync(c => c.EInvoiceId == dto.EInvoiceId);

            if (eInvoice is null) return BadRequest();

            try
            {

                _context.Entry(eInvoice).State = EntityState.Modified;

                eInvoice.EInvoiceId = dto.EInvoiceId;
                eInvoice.CustomerId = dto.CustomerId.Value;
                eInvoice.EInvoiceCode = dto.EInvoiceCode;
                eInvoice.DateTimeIssued = dto.DateTimeIssued;
                eInvoice.EInvoiceTypeId = dto.EInvoiceTypeId.Value;
                eInvoice.NetAmount = dto.NetAmount;

                foreach (var line in dto.EInvoiceLines)
                {
                    foreach (var item in line.EInvoiceLineTaxes.GroupBy(x => x.TaxId).Select(x => new { x.Key, Taxes = x }))
                    {
                        if (item.Taxes.Count() > 1) throw new Exception($"Item {line.ItemName} with Net Amount {line.ItemNetAmount} has duplicated taxes.");
                    }
                    var eInvoiceLine = eInvoice.EInvoiceLines.FirstOrDefault(c => c.EInvoiceLineId == line.EInvoiceLineId);
                    if (eInvoiceLine is null)
                    {
                        eInvoiceLine = new()
                        {
                            EInvoiceLineId = line.EInvoiceLineId.Value,
                            EInvoiceId = line.EInvoiceId.Value,
                            AmountSold = line.AmountSold.Value,
                            ItemId = line.ItemId.Value,
                            Quantity = line.Quantity.Value,
                            Total = line.Total.Value,
                            ItemNetAmount = line.ItemNetAmount.Value,
                        };
                        eInvoice.EInvoiceLines.Add(eInvoiceLine);
                        _context.Entry(eInvoiceLine).State = EntityState.Added;
                        foreach (var tax in line.EInvoiceLineTaxes)
                        {
                            EInvoiceLineTax eInvoiceLineTax = new()
                            {
                                EInvoiceLineTaxId = tax.EInvoiceLineTaxId,
                                EInvoiceLineId = tax.EInvoiceLineId.Value,
                                TaxId = tax.TaxId.Value,
                                Amount = tax.Amount.Value,
                            };
                            eInvoiceLine.EInvoiceLineTaxes.Add(eInvoiceLineTax);
                            _context.Entry(eInvoiceLineTax).State = EntityState.Added;
                        }
                    }
                    else
                    {
                        _context.Entry(eInvoiceLine).State = EntityState.Modified;

                        eInvoiceLine.EInvoiceLineId = line.EInvoiceLineId.Value;
                        eInvoiceLine.EInvoiceId = line.EInvoiceId.Value;
                        eInvoiceLine.AmountSold = line.AmountSold.Value;
                        eInvoiceLine.ItemId = line.ItemId.Value;
                        eInvoiceLine.Quantity = line.Quantity.Value;
                        eInvoiceLine.Total = line.Total.Value;
                        eInvoiceLine.ItemNetAmount = line.ItemNetAmount.Value;
                        foreach (var tax in line.EInvoiceLineTaxes)
                        {
                            var eInvoiceLineTax = eInvoiceLine.EInvoiceLineTaxes.FirstOrDefault(c => c.EInvoiceLineTaxId == tax.EInvoiceLineTaxId);
                            if (eInvoiceLineTax is null)
                            {
                                eInvoiceLineTax = new()
                                {
                                    EInvoiceLineTaxId = tax.EInvoiceLineTaxId,
                                    EInvoiceLineId = tax.EInvoiceLineId.Value,
                                    TaxId = tax.TaxId.Value,
                                    Amount = tax.Amount.Value,
                                };
                                eInvoiceLine.EInvoiceLineTaxes.Add(eInvoiceLineTax);
                                _context.Entry(eInvoiceLineTax).State = EntityState.Added;
                            }
                            else
                            {
                                _context.Entry(eInvoiceLineTax).State = EntityState.Modified;

                                eInvoiceLineTax.EInvoiceLineTaxId = tax.EInvoiceLineTaxId;
                                eInvoiceLineTax.EInvoiceLineId = tax.EInvoiceLineId.Value;
                                eInvoiceLineTax.TaxId = tax.TaxId.Value;
                                eInvoiceLineTax.Amount = tax.Amount.Value;
                            }
                        }
                        var deletedLineTaxes = eInvoiceLine.EInvoiceLineTaxes.Where(c => !line.EInvoiceLineTaxes.Any(x => c.EInvoiceLineTaxId == x.EInvoiceLineTaxId));
                        if (deletedLineTaxes.Any())
                        {
                            foreach (var item in deletedLineTaxes)
                            {
                                _context.Entry(item).State = EntityState.Deleted;
                                //eInvoiceLine.EInvoiceLineTaxes.Remove(item);
                                _context.EInvoiceLineTaxes.Remove(item);
                            }
                        }
                    }
                }
                var deletedLines = eInvoice.EInvoiceLines.Where(c => !dto.EInvoiceLines.Any(x => c.EInvoiceLineId == x.EInvoiceLineId)).ToList();
                if (deletedLines.Any())
                {
                    foreach (var item in deletedLines)
                    {
                        _context.Entry(item).State = EntityState.Deleted;
                        //eInvoice.EInvoiceLines.Remove(item);
                        _context.EInvoiceLines.Remove(item);
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (!EInvoiceExists(dto.EInvoiceId))
                    return NotFound("EInvoice not found!");
                else
                    return BadRequest(ex.Message);
            }

            return Content("Saved Successfully");
        }

        // POST: api/EInvoices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostEInvoice(EInvoiceDto dto)
        {
            if (_context.EInvoices == null)
            {
                return Problem("Entity set 'EInvoiceContext.EInvoices'  is null.");
            }

            try
            {
                EInvoice eInvoice = new()
                {
                    EInvoiceId = dto.EInvoiceId,
                    CustomerId = dto.CustomerId.Value,
                    EInvoiceCode = dto.EInvoiceCode,
                    DateTimeIssued = dto.DateTimeIssued,
                    EInvoiceTypeId = dto.EInvoiceTypeId.Value,
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
                        EInvoiceLineId = line.EInvoiceLineId.Value,
                        EInvoiceId = line.EInvoiceId.Value,
                        AmountSold = line.AmountSold.Value,
                        ItemId = line.ItemId.Value,
                        Quantity = line.Quantity.Value,
                        Total = line.Total.Value,
                        ItemNetAmount = line.ItemNetAmount.Value,
                    };
                    foreach (var tax in line.EInvoiceLineTaxes)
                    {
                        EInvoiceLineTax eInvoiceLineTax = new()
                        {
                            EInvoiceLineTaxId = tax.EInvoiceLineTaxId,
                            EInvoiceLineId = tax.EInvoiceLineId.Value,
                            TaxId = tax.TaxId.Value,
                            Amount = tax.Amount.Value,
                        };
                        eInvoiceLine.EInvoiceLineTaxes.Add(eInvoiceLineTax);
                    }
                    eInvoice.EInvoiceLines.Add(eInvoiceLine);
                }
                _context.EInvoices.Add(eInvoice);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (EInvoiceExists(dto.EInvoiceId))
                    return Conflict("EInvoice is invalid!");
                else
                    return BadRequest(ex.Message);
            }

            return CreatedAtAction("GetEInvoice", new { id = dto.EInvoiceId }, dto);
        }

        // DELETE: api/EInvoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEInvoice(Guid id)
        {
            var eInvoice = await _context.EInvoices.FindAsync(id);
            if (eInvoice == null) return NotFound();

            _context.EInvoices.Remove(eInvoice);
            await _context.SaveChangesAsync();

            return Content("Deleted Successfully");
        }

        private bool EInvoiceExists(Guid id)
        {
            return (_context.EInvoices?.Any(e => e.EInvoiceId == id)).GetValueOrDefault();
        }
    }
}
