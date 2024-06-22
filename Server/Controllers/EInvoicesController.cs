using Microsoft.AspNetCore.Mvc;
using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Models;
using EInvoiceDemo.Server.Repositories;

namespace EInvoiceDemo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EInvoicesController : GenericController<IEInvoiceRepository>
    {
        public EInvoicesController(IEInvoiceRepository eInvoiceRepository) : base(eInvoiceRepository) { }

        // GET: api/EInvoices/Code
        [HttpGet("Code")]
        public async Task<ActionResult<int>> GetEInvoiceCode() => await _repository.GetCode();

        // GET: api/EInvoices
        [HttpPost("{filter}")]
        public async Task<ActionResult<EInvoicesFilter>> GetEInvoices(EInvoicesFilter? filter) => await _repository.GetList(filter);
     
        // GET: api/EInvoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EInvoiceDto>> GetEInvoice(Guid id) => await _repository.GetSingle(id);
     
        // PUT: api/EInvoices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutEInvoice(EInvoiceDto dto)
        {
            await _repository.Update(dto);
            return Content("Saved Successfully");
        }

        // POST: api/EInvoices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostEInvoice(EInvoiceDto dto)
        {
            await _repository.Add(_repository.AddLogic(dto));
            return CreatedAtAction("GetEInvoice", new { id = dto.Id }, dto);
        }

        // DELETE: api/EInvoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEInvoice(Guid id)
        {
            await _repository.Delete(id);
            return Content("Deleted Successfully");
        }

        // DELETE: api/EInvoices/5
        [HttpPost("Bulk")]
        public async Task<IActionResult> DeleteEInvoices(Bulk ids) => Content(await _repository.Bulk(ids));
    }
}
