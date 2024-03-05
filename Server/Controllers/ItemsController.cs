using Microsoft.AspNetCore.Mvc;
using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Models;
using EInvoiceDemo.Server.Repositories;

namespace EInvoiceDemo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : GenericController<IItemRepository>
    {
        public ItemsController(IItemRepository itemRepository) : base(itemRepository) { }

        // GET: api/Items/KeyValue
        [HttpGet("KeyValue")]
        public async Task<ActionResult<List<KeyValue>>> GetKeyValue([FromQuery] string? filter) => await _repository.GetKeyValue(filter);

        // GET: api/Items/Code
        [HttpGet("Code")]
        public async Task<ActionResult<int>> GetItemCode() => await _repository.GetCode();

        // GET: api/Items
        [HttpPost("{filter}")]
        public async Task<ActionResult<ItemsFilter>> GetItems(ItemsFilter? filter) => await _repository.GetList(filter);

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItem(Guid id) => await _repository.GetSingle(id);

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutItem(ItemDto dto)
        {
            await _repository.Update(dto);
            return Ok("Saved Successfully");
        }

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostItem(Item item)
        {
            await _repository.Add(item);
            return CreatedAtAction("GetItem", new { id = item.ItemId }, item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            await _repository.Delete(id);
            return Ok("Deleted Successfully");
        }
    }
}
