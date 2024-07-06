using Microsoft.AspNetCore.Mvc;
using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Models;
using EInvoiceDemo.Server.Repositories;
using EInvoiceDemo.Shared.Helpers;

namespace EInvoiceDemo.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController(IGenericRepository<Customer, CustomerDto, CustomersFilter> customerRepository): ControllerBase
{
    // GET: api/Customers/KeyValue
    [HttpGet("KeyValue")]
    public async Task<ActionResult<List<KeyValue>>> GetKeyValue([FromQuery] string? filter)
        => await customerRepository.GetKeyValue(filter, c => (c.Code + " " + c.CustomerName).Contains(filter!));
    
    // GET: api/Customers/Code
    [HttpGet("Code")]
    public async Task<ActionResult<int>> GetCustomerCode() => await customerRepository.GetCode();

    // GET: api/Customers
    [HttpPost("{filter}")]
    public async Task<ActionResult<CustomersFilter>> GetCustomers(CustomersFilter? filter)
        => await customerRepository.GetList(filter, 
                () => customerRepository.Query()
                    .WhereIf(filter.CustomerName.HasValue(), c => (c.Code + " " + c.CustomerName).Contains(filter.CustomerName!))
        );
    
    // GET: api/Customers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDto>> GetCustomer(Guid id) => await customerRepository.GetSingle(id);
    
    // PUT: api/Customers/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut]
    public async Task<IActionResult> PutCustomer(CustomerDto dto)
    {
        await customerRepository.Update(dto);
        return Ok("Saved Successfully");
    }

    // POST: api/Customers
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<IActionResult> PostCustomer(Customer customer)
    {
        await customerRepository.Add(customer);
        return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
    }

    // DELETE: api/Customers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        await customerRepository.Delete(id);
        return Ok("Deleted Successfully");
    }
}
