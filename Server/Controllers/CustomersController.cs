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
    public class CustomersController : ControllerBase
    {
        private readonly EInvoiceContext _context;

        public CustomersController(EInvoiceContext context)
        {
            _context = context;
        }

        // GET: api/Customers/KeyValue
        [HttpGet("KeyValue")]
        public async Task<ActionResult<List<KeyValue>>> GetKeyValue([FromQuery] string? filter)
        {
            var data = _context.Customers.AsQueryable();
            if (filter.HasValue())
                data = data.Where(c => (c.CustomerCode + " " + c.CustomerName).Contains(filter));
            return await data
            .Select(c => new KeyValue
            {
                Key = c.CustomerId,
                Value = c.CustomerCode + " - " + c.CustomerName
            }).Take(30).ToListAsync();

        }

        // GET: api/Customers/Code
        [HttpGet("Code")]
        public async Task<ActionResult<int>> GetCustomerCode()
            => (await _context.Customers?.MaxAsync(c => (int?)c.CustomerCode) ?? 0) + 1;

        // GET: api/Customers
        [HttpPost("{filter}")]
        public async Task<ActionResult<CustomersFilter>> GetCustomers(CustomersFilter? filter)
        {
            var query = _context.Customers.AsQueryable();
            if (filter is null) filter = new CustomersFilter();
            else
            {
                if (filter.CustomerName.HasValue())
                    query = query.Where(c => (c.CustomerCode + " " + c.CustomerName).Contains(filter.CustomerName));
            }
            filter.Pagination = Pagination.GetPagination<Customer, CustomersFilter, CustomerDto>(query, filter);

            filter.Items = await query
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    CustomerCode = c.CustomerCode,
                })
                .OrderWith(filter.SortField, filter.SortApproach)
                .Skip(filter.Pagination.PageNo * filter.Pagination.RowsCount)
                .Take(filter.Pagination.RowsCount)
                .ToListAsync();

            return filter;
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null) return NotFound();

            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                CustomerCode = customer.CustomerCode,
                CustomerName = customer.CustomerName,
            };
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutCustomer(Customer dto)
        {
            var Customer = await _context.Customers.FindAsync(dto.CustomerId);

            if (Customer is null) return BadRequest();

            Customer.CustomerCode = dto.CustomerCode;
            Customer.CustomerName = dto.CustomerName;

            _context.Entry(Customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(dto.CustomerId))
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

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostCustomer(Customer customer)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'EInvoiceContext.Customers'  is null.");
            }
            _context.Customers.Add(customer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.CustomerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return NotFound();
            if (_context.EInvoices.Any(c => c.CustomerId == id))
                return BadRequest("This Customer used with E-Invoice and cannot be deleted.");

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok("Deleted Successfully");
        }

        private bool CustomerExists(Guid id)
        {
            return (_context.Customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }
    }
}
