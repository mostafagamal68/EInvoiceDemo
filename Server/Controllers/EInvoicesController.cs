using EInvoiceDemo.Server.Handlers;
using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Helpers;
using EInvoiceDemo.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace EInvoiceDemo.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EInvoicesController(IEInvoiceHandler handler) : ControllerBase
{

    // GET: api/EInvoices/Code
    [HttpGet("Code")]
    public async Task<ActionResult<int>> GetEInvoiceCode() => await handler.GetCode();

    // GET: api/EInvoices
    [HttpPost("{filter}")]
    public async Task<ActionResult<EInvoicesFilter>> GetEInvoices(EInvoicesFilter? filter)
        => await handler.GetList(filter,
            c => c.WhereIf(filter.CustomerId.HasValue, c => c.CustomerId == filter.CustomerId)
                .WhereIf(filter.EInvoiceType.HasValue, c => c.EInvoiceType == filter.EInvoiceType)
                .WhereIf(filter.DateTimeIssuedFrom.HasValue, c => c.DateTimeIssued >= filter.DateTimeIssuedFrom)
                .WhereIf(filter.DateTimeIssuedTo.HasValue, c => c.DateTimeIssued <= filter.DateTimeIssuedTo)
        );
 
    // GET: api/EInvoices/5
    [HttpGet("{id}")]
    public async Task<ActionResult<EInvoiceDto>> GetEInvoice(Guid id) => await handler.GetSingle(id);
 
    // PUT: api/EInvoices/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut]
    public async Task<IActionResult> PutEInvoice(EInvoiceDto dto)
    {
        await handler.Edit(dto);
        return Content("Saved Successfully");
    }

    // POST: api/EInvoices
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<IActionResult> PostEInvoice(EInvoiceDto dto)
    {
        await handler.Create(dto);
        return CreatedAtAction("GetEInvoice", new { id = dto.Id }, dto);
    }

    // DELETE: api/EInvoices/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEInvoice(Guid id)
    {
        await handler.Delete(id);
        return Content("Deleted Successfully");
    }

    // DELETE: api/EInvoices/5
    [HttpPost("Bulk")]
    public async Task<IActionResult> BulkEInvoices(Bulk model) => Content(await handler.Bulk(model));
}
