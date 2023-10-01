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

namespace EInvoiceDemo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EInvoiceTypesController : ControllerBase
    {
        private readonly EInvoiceContext _context;

        public EInvoiceTypesController(EInvoiceContext context)
        {
            _context = context;
        }

        // GET: api/EInvoiceTypes
        [HttpPost("{filter}")]
        public async Task<ActionResult<EInvoiceTypesFilter>> GetEInvoiceTypes(EInvoiceTypesFilter? filter)
        {
            var query = _context.EInvoiceTypes.AsQueryable();
            if (filter is null) filter = new EInvoiceTypesFilter();
            if (filter.EInvoiceTypeName.HasValue())
                query = query.Where(c => c.EInvoiceTypeName.Contains(filter.EInvoiceTypeName));
            var list = await query
                .Select(c => new EInvoiceTypeDto
                {
                    EInvoiceTypeId = c.EInvoiceTypeId,
                    EInvoiceTypeName = c.EInvoiceTypeName,
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

        // GET: api/EInvoiceTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EInvoiceTypeDto>> GetEInvoiceType(Guid? id)
        {
            var eInvoiceType = await _context.EInvoiceTypes.FindAsync(id);

            if (eInvoiceType is null) return NotFound();

            return new EInvoiceTypeDto
            {
                EInvoiceTypeId = eInvoiceType.EInvoiceTypeId,
                EInvoiceTypeName = eInvoiceType?.EInvoiceTypeName,
            };
        }

        // PUT: api/EInvoiceTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutEInvoiceType(EInvoiceTypeDto dto)
        {
            EInvoiceType eInvoiceType = new()
            {
                EInvoiceTypeId = dto.EInvoiceTypeId,
                EInvoiceTypeName = dto.EInvoiceTypeName
            };

            _context.Entry(eInvoiceType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EInvoiceTypeExists(dto.EInvoiceTypeId))
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

        // POST: api/EInvoiceTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostEInvoiceType(EInvoiceType eInvoiceType)
        {
            if (_context.EInvoiceTypes == null)
            {
                return Problem("Entity set 'EInvoiceContext.EInvoiceTypes'  is null.");
            }
            _context.EInvoiceTypes.Add(eInvoiceType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EInvoiceTypeExists(eInvoiceType.EInvoiceTypeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEInvoiceType", new { id = eInvoiceType.EInvoiceTypeId }, eInvoiceType);
        }

        // DELETE: api/EInvoiceTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEInvoiceType(Guid id)
        {
            var eInvoiceType = await _context.EInvoiceTypes.FindAsync(id);
            if (eInvoiceType == null)
                return NotFound();

            _context.EInvoiceTypes.Remove(eInvoiceType);
            await _context.SaveChangesAsync();

            return Ok("Deleted Successfully");
        }

        private bool EInvoiceTypeExists(Guid id)
        {
            return (_context.EInvoiceTypes?.Any(e => e.EInvoiceTypeId == id)).GetValueOrDefault();
        }
    }
}
