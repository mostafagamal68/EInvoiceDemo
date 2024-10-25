using EInvoiceDemo.Server.Handlers;
using EInvoiceDemo.Server.Models;
using EInvoiceDemo.Shared.DTOs;
using EInvoiceDemo.Shared.Helpers;
using EInvoiceDemo.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace EInvoiceDemo.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemsController(IGenericHandler<Item, ItemDto, ItemsFilter> handler) : ControllerBase
{
    // GET: api/Items/KeyValue
    [HttpGet("KeyValue")]
    public async Task<ActionResult<List<KeyValue>>> GetKeyValue([FromQuery] string? filter)
        => await handler.GetKeyValue(filter, c => (c.Code + " " + c.ItemName).Contains(filter!));

    // GET: api/Items/Code
    [HttpGet("Code")]
    public async Task<ActionResult<int>> GetItemCode() => await handler.GetCode();

    // GET: api/Items
    [HttpPost("{filter}")]
    public async Task<ActionResult<ItemsFilter>> GetItems(ItemsFilter filter)
        => await handler.GetList(filter,
            c => c.WhereIf(filter.ItemName.HasValue(), c => (c.Code + " " + c.ItemName).Contains(filter.ItemName!))
        );

    // GET: api/Items/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetItem(Guid id) => await handler.GetSingle(id);

    // PUT: api/Items/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut]
    public async Task<IActionResult> PutItem(ItemDto dto)
    {
        await handler.Edit(dto);
        return Ok("Saved Successfully");
    }

    // POST: api/Items
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<IActionResult> PostItem(ItemDto item)
    {
        await handler.Create(item);
        return CreatedAtAction("GetItem", new { id = item.Id }, item);
    }

    // DELETE: api/Items/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItem(Guid id)
    {
        await handler.Delete(id);
        return Ok("Deleted Successfully");
    }
}
