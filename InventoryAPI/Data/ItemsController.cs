using Microsoft.AspNetCore.Mvc;

namespace InventoryAPI.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ItemsController : ControllerBase
  {
    private readonly InventoryContext _context;
    public ItemsController(InventoryContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> GetItems()
    {
      return await _context.items.ToListAsync();
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<Item>> GetItem(int Id)
    {
      var item = await _context.items.FindAsync(Id);
      if (item == null)
      {
        return NotFound();
      }
      return item;
    }

    [HttpPost]
    public async Task<ActionResult<Item>> AddItem(Item item)
    {
      _context.items.Add(item);
      await _context.SaveChangesAsync();
      return CreatedAtAction(nameof(GetItem), new { id = item.id }, item);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateItem (int Id, Item item)
    {
      if (Id == item.Id)
      {
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
      }
      else
      {
        return BadRequest();
      }
    }

    [HttpDelete("Id")]
    public async Task<IActionResult> Delete(int Id)
    {
      var item = await _context.items.FindAsync(Id);

      if (item == null){
        return NotFound();
      }

      _context.items.Remove(item);
      await _context.SaveChangesAsync();
      return NoContent();
    }

    //Here you can add Delete All button too
  }
}