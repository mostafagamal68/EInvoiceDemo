using EInvoiceDemo.Server.Handlers;
using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Shared.DTOs.Customers;
using EInvoiceDemo.Shared.Helpers;
using EInvoiceDemo.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EInvoiceDemo.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CustomersController(IGenericHandler<Customer, CustomerDto, CustomersFilter> handler) : ControllerBase
{
    // GET: api/Customers/KeyValue
    [HttpGet("KeyValue")]
    public async Task<ActionResult<List<KeyValue>>> GetKeyValue([FromQuery] string? filter)
        => await handler.GetKeyValue(filter, c => (c.Code + " " + c.CustomerName).Contains(filter!));

    // GET: api/Customers/Code
    [HttpGet("Code")]
    public async Task<ActionResult<int>> GetCustomerCode() => await handler.GetCode();

    // GET: api/Customers
    [HttpPost("{filter}")]
    public async Task<ActionResult<CustomersFilter>> GetCustomers(CustomersFilter filter)
        => await handler.GetList(filter,
            c => c.WhereIf(filter.CustomerName.HasValue(), c => (c.Code + " " + c.CustomerName).Contains(filter.CustomerName!))
        );

    // GET: api/Customers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDto>> GetCustomer(Guid id) => await handler.GetSingle(id);

    // PUT: api/Customers/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut]
    public async Task<IActionResult> PutCustomer(CustomerDto dto)
    {
        await handler.Edit(dto);
        return Ok("Saved Successfully");
    }

    // POST: api/Customers
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<IActionResult> PostCustomer(CustomerDto customer)
    {
        await handler.Create(customer);
        return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
    }

    // DELETE: api/Customers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        await handler.Delete(id);
        return Ok("Deleted Successfully");
    }
}
