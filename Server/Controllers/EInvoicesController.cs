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
using System.Data.Common;
using EInvoiceDemo.Shared.Helpers;

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
            var list = await query
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
                .Skip(filter.Pagination.PageNo * filter.Pagination.RowsCount)
                .Take(filter.Pagination.RowsCount)
                .ToListAsync();

            filter.Items = list;
            filter.Pagination = new Pagination
            {
                PageNo = filter.Pagination.PageNo,
                CurrentPage = filter.Pagination.PageNo,
                RowsCount = filter.Pagination.RowsCount,
                TotalRows = query.Count(),
                PagesCount = (int)Math.Ceiling((decimal)query.Count() / filter.Pagination.RowsCount)
            };

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
            var eInvoice = await _context.EInvoices.FindAsync(dto.EInvoiceId);

            if (eInvoice is null) return BadRequest();

            _context.Entry(eInvoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbException ex)
            {
                if (!EInvoiceExists(dto.EInvoiceId))
                    return NotFound("EInvoice not found!");
                else
                    BadRequest(ex.Message);
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
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (EInvoiceExists(dto.EInvoiceId))
                    return Conflict("EInvoice is invalid!");
                else
                    BadRequest(ex.Message);
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
