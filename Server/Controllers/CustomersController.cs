using Microsoft.AspNetCore.Mvc;
using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Models;
using EInvoiceDemo.Server.Repositories;

namespace EInvoiceDemo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(ICustomerRepository customerRepository)
        : GenericController<ICustomerRepository>(customerRepository)
    {

        // GET: api/Customers/KeyValue
        [HttpGet("KeyValue")]
        public async Task<ActionResult<List<KeyValue>>> GetKeyValue([FromQuery] string? filter) => await _repository.GetKeyValue(filter);
        
        // GET: api/Customers/Code
        [HttpGet("Code")]
        public async Task<ActionResult<int>> GetCustomerCode() => await _repository.GetCode();

        // GET: api/Customers
        [HttpPost("{filter}")]
        public async Task<ActionResult<CustomersFilter>> GetCustomers(CustomersFilter? filter) => await _repository.GetList(filter);
        
        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(Guid id) => await _repository.GetSingle(id);
        
        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutCustomer(CustomerDto dto)
        {
            await _repository.Update(dto);
            return Ok("Saved Successfully");
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostCustomer(Customer customer)
        {
            await _repository.Add(customer);
            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            await _repository.Delete(id);
            return Ok("Deleted Successfully");
        }
    }
}
