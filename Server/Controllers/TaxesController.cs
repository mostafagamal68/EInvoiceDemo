using EInvoiceDemo.Server.Handlers;
using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Helpers;
using EInvoiceDemo.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace EInvoiceDemo.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaxesController(IGenericHandler<Tax, TaxDto, TaxesFilter> taxRepository) : ControllerBase
{
    // GET: api/Taxes/KeyValue
    [HttpGet("KeyValue")]
    public async Task<ActionResult<List<KeyValue>>> GetKeyValue([FromQuery] string? filter)
        => await taxRepository.GetKeyValue(filter, c => (c.Code + " " + c.TaxName).Contains(filter!));

    // GET: api/Taxes/Code
    [HttpGet("Code")]
    public async Task<ActionResult<int>> GetTaxCode() => await taxRepository.GetCode();

    // GET: api/Taxes
    [HttpPost("{filter}")]
    public async Task<ActionResult<TaxesFilter>> GetTaxes(TaxesFilter filter)
        => await taxRepository.GetList(filter,
                c => c.WhereIf(filter.TaxName.HasValue(), c => (c.Code + " " + c.TaxName).Contains(filter.TaxName!))
        );

    // GET: api/Taxes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TaxDto>> GetTax(Guid id) => await taxRepository.GetSingle(id);

    // PUT: api/Taxes/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut]
    public async Task<IActionResult> PutTax(TaxDto dto)
    {
        await taxRepository.Edit(dto);
        return Ok("Saved Successfully");
    }

    // POST: api/Taxes
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Tax>> PostTax(TaxDto tax)
    {
        await taxRepository.Create(tax);
        return CreatedAtAction("GetTax", new { id = tax.Id }, tax);
    }

    // DELETE: api/Taxes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTax(Guid id)
    {
        await taxRepository.Delete(id);
        return Ok("Deleted Successfully");
    }
}
