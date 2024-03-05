using Microsoft.AspNetCore.Mvc;
using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Models;
using EInvoiceDemo.Server.Repositories;

namespace EInvoiceDemo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EInvoiceTypesController : GenericController<IEInvoiceTypeRepository>
    {
        public EInvoiceTypesController(IEInvoiceTypeRepository repository) : base(repository) { }

        // GET: api/EInvoiceTypes/KeyValue
        [HttpGet("KeyValue")]
        public async Task<ActionResult<List<KeyValue>>> GetKeyValue([FromQuery] string? filter) => await _repository.GetKeyValue(filter);

        // GET: api/EInvoiceTypes
        [HttpPost("{filter}")]
        public async Task<ActionResult<EInvoiceTypesFilter>> GetEInvoiceTypes(EInvoiceTypesFilter? filter) => await _repository.GetList(filter);

        // GET: api/EInvoiceTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EInvoiceTypeDto>> GetEInvoiceType(Guid id) => await _repository.GetSingle(id);

        // PUT: api/EInvoiceTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutEInvoiceType(EInvoiceTypeDto dto)
        {
            await _repository.Update(dto);
            return Ok("Saved Successfully");
        }

        // POST: api/EInvoiceTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostEInvoiceType(EInvoiceType eInvoiceType)
        {
            await _repository.Add(eInvoiceType);
            return CreatedAtAction("GetEInvoiceType", new { id = eInvoiceType.EInvoiceTypeId }, eInvoiceType);
        }

        // DELETE: api/EInvoiceTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEInvoiceType(Guid id)
        {
            await _repository.Delete(id);
            return Ok("Deleted Successfully");
        }
    }
}
