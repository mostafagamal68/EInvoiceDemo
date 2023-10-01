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
using EInvoiceDemo.Shared.Helpers;
using EInvoiceDemo.Client.Pages.Customer;

namespace EInvoiceDemo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxesController : ControllerBase
    {
        private readonly EInvoiceContext _context;

        public TaxesController(EInvoiceContext context)
        {
            _context = context;
        }

        // GET: api/Taxes
        [HttpPost("{filter}")]
        public async Task<ActionResult<TaxesFilter>> GetTaxes(TaxesFilter filter)
        {
            var query = _context.Taxes.AsQueryable();
            if (filter is null) filter = new TaxesFilter();
            if (filter.TaxName.HasValue())
                query = query.Where(c => (c.TaxCode + " " + c.TaxName).Contains(filter.TaxName));
            var list = await query
                .Select(c => new TaxDto
                {
                    TaxId = c.TaxId,
                    TaxName = c.TaxName,
                    TaxCode = c.TaxCode,
                    TaxDescription = c.TaxDescription,
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

        // GET: api/Taxes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tax>> GetTax(Guid id)
        {
            var tax = await _context.Taxes.FindAsync(id);

            if (tax == null) return NotFound();

            return new TaxDto
            {
                TaxId = c.TaxId,
                TaxName = c.TaxName,
                TaxCode = c.TaxCode,
                TaxDescription = c.TaxDescription,
            };
        }

        // PUT: api/Taxes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutTax(TaxDto dto)
        {
            var Tax = await _context.Taxes.FindAsync(dto.TaxId);

            if (Tax is null) return BadRequest();

            Tax.TaxName = dto.TaxName;
            Tax.TaxCode = dto.TaxCode;
            Tax.TaxDescription = dto.TaxDescription;

            _context.Entry(Tax).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaxExists(dto.TaxId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Saved Successfully");
        }

        // POST: api/Taxes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tax>> PostTax(Tax tax)
        {
            if (_context.Taxes == null)
            {
                return Problem("Entity set 'EInvoiceContext.Taxes'  is null.");
            }
            _context.Taxes.Add(tax);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TaxExists(tax.TaxId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTax", new { id = tax.TaxId }, tax);
        }

        // DELETE: api/Taxes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTax(Guid id)
        {
            var tax = await _context.Taxes.FindAsync(id);
            if (tax == null) return NotFound();

            _context.Taxes.Remove(tax);
            await _context.SaveChangesAsync();

            return Ok("Deleted Successfully");
        }

        private bool TaxExists(Guid id)
        {
            return (_context.Taxes?.Any(e => e.TaxId == id)).GetValueOrDefault();
        }
    }
}
