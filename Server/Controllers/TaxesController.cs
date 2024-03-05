using Microsoft.AspNetCore.Mvc;
using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Models;
using EInvoiceDemo.Server.Repositories;

namespace EInvoiceDemo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxesController : GenericController<ITaxRepository>
    {
        public TaxesController(ITaxRepository taxRepository) : base(taxRepository) { }

        // GET: api/Taxes/KeyValue
        [HttpGet("KeyValue")]
        public async Task<ActionResult<List<KeyValue>>> GetKeyValue([FromQuery] string? filter) => await _repository.GetKeyValue(filter);

        // GET: api/Taxes/Code
        [HttpGet("Code")]
        public async Task<ActionResult<int>> GetTaxCode() => await _repository.GetCode();

        // GET: api/Taxes
        [HttpPost("{filter}")]
        public async Task<ActionResult<TaxesFilter>> GetTaxes(TaxesFilter filter) => await _repository.GetList(filter);

        // GET: api/Taxes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaxDto>> GetTax(Guid id) => await _repository.GetSingle(id);

        // PUT: api/Taxes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutTax(TaxDto dto)
        {
            await _repository.Update(dto);
            return Ok("Saved Successfully");
        }

        // POST: api/Taxes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tax>> PostTax(Tax tax)
        {
            await _repository.Add(tax);
            return CreatedAtAction("GetTax", new { id = tax.TaxId }, tax);
        }

        // DELETE: api/Taxes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTax(Guid id)
        {
            await _repository.Delete(id);
            return Ok("Deleted Successfully");
        }
    }
}
