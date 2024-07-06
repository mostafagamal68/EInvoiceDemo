using Microsoft.AspNetCore.Mvc;
using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Models;
using EInvoiceDemo.Server.Repositories;

namespace EInvoiceDemo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EInvoicesController(IEInvoiceRepository repository) : ControllerBase
    {

        // GET: api/EInvoices/Code
        [HttpGet("Code")]
        public async Task<ActionResult<int>> GetEInvoiceCode() => await repository.GetCode();

        // GET: api/EInvoices
        [HttpPost("{filter}")]
        public async Task<ActionResult<EInvoicesFilter>> GetEInvoices(EInvoicesFilter? filter) => await repository.GetList(filter, null);
     
        // GET: api/EInvoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EInvoiceDto>> GetEInvoice(Guid id) => await repository.GetSingle(id);
     
        // PUT: api/EInvoices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutEInvoice(EInvoiceDto dto)
        {
            await repository.Update(dto);
            return Content("Saved Successfully");
        }

        // POST: api/EInvoices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostEInvoice(EInvoiceDto dto)
        {
            await repository.Add(repository.AddLogic(dto));
            return CreatedAtAction("GetEInvoice", new { id = dto.Id }, dto);
        }

        // DELETE: api/EInvoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEInvoice(Guid id)
        {
            await repository.Delete(id);
            return Content("Deleted Successfully");
        }

        // DELETE: api/EInvoices/5
        [HttpPost("Bulk")]
        public async Task<IActionResult> DeleteEInvoices(Bulk ids) => Content(await repository.Bulk(ids));
    }
}
