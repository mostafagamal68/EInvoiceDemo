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
using EInvoiceDemo.Shared.Models;

namespace EInvoiceDemo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly EInvoiceContext _context;

        public ItemsController(EInvoiceContext context)
        {
            _context = context;
        }

        // GET: api/Items/KeyValue
        [HttpGet("KeyValue")]
        public async Task<ActionResult<List<KeyValue>>> GetKeyValue([FromQuery] string? filter)
        {
            var data = _context.Items.AsQueryable();
            if (filter.HasValue())
                data = data.Where(c => (c.ItemCode + " " + c.ItemName).Contains(filter));
            return await data
            .Select(c => new KeyValue
            {
                Key = c.ItemId,
                Value = c.ItemCode + " - " + c.ItemName
            }).Take(30).ToListAsync();

        }

        // GET: api/Items/Code
        [HttpGet("Code")]
        public async Task<ActionResult<int>> GetItemCode()
            => (await _context.Items?.MaxAsync(c => (int?)c.ItemCode) ?? 0) + 1;

        // GET: api/Items
        [HttpPost("{filter}")]
        public async Task<ActionResult<ItemsFilter>> GetItems(ItemsFilter? filter)
        {
            var query = _context.Items.AsQueryable();
            if (filter is null) filter = new ItemsFilter();
            if (filter.ItemName.HasValue())
                query = query.Where(c => (c.ItemCode + " " + c.ItemName).Contains(filter.ItemName));
            var list = await query
                .Select(c => new ItemDto
                {
                    ItemId = c.ItemId,
                    ItemName = c.ItemName,
                    ItemCode = c.ItemCode,
                    ItemDescription = c.ItemDescription,
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

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItem(Guid id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return new ItemDto
            {
                ItemId = item.ItemId,
                ItemName = item.ItemName,
                ItemCode = item.ItemCode,
                ItemDescription = item.ItemDescription,
            };
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutItem(ItemDto dto)
        {
            var Item = await _context.Items.FindAsync(dto.ItemId);

            if (Item is null) return BadRequest();

            Item.ItemName = dto.ItemName;
            Item.ItemCode = dto.ItemCode;
            Item.ItemDescription = dto.ItemDescription;

            _context.Entry(Item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(dto.ItemId))
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

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostItem(Item item)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'EInvoiceContext.Items'  is null.");
            }
            _context.Items.Add(item);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ItemExists(item.ItemId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetItem", new { id = item.ItemId }, item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null) return NotFound();

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return Ok("Deleted Successfully");
        }

        private bool ItemExists(Guid id)
        {
            return (_context.Items?.Any(e => e.ItemId == id)).GetValueOrDefault();
        }
    }
}
