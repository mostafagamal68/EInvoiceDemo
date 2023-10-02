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
            .Include(c => c.EInvoiceLines).ThenInclude(c => c.EInvoiceLineTaxes)
            .AsQueryable();


        // GET: api/EInvoices/Code
        [HttpGet]
        [Route("Code")]
        public async Task<ActionResult<int>> GetEInvoiceCode()
            => (await _context.EInvoices?.MaxAsync(c => (int?)c.EInvoiceCode) ?? 0) + 1;

        // GET: api/EInvoices
        [HttpPost("{filter}")]
        public async Task<ActionResult<EInvoicesFilter>> GetEInvoices(EInvoicesFilter? filter)
        {
            var query = Query();

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
            };
        }

        // PUT: api/EInvoices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutEInvoice(EInvoiceDto dto)
        {
            var eInvoice = await _context.EInvoices.FindAsync(dto.EInvoiceId);

            if (eInvoice is null) return BadRequest();

            _context.Entry(dto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EInvoiceExists(dto.EInvoiceId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Content("Saved Successfully");
        }

        // POST: api/EInvoices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostEInvoice(EInvoice eInvoice)
        {
            if (_context.EInvoices == null)
            {
                return Problem("Entity set 'EInvoiceContext.EInvoices'  is null.");
            }
            _context.EInvoices.Add(eInvoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEInvoice", new { id = eInvoice.EInvoiceId }, eInvoice);
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
