using InventoryAPI.Data; 
using InventoryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
      return await _context.Items.ToListAsync();
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<Item>> GetItem(int Id)
    {
      var item = await _context.Items.FindAsync(Id);
      if (item == null)
      {
        return NotFound();
      }
      return item;
    }

    [HttpPost]
    public async Task<ActionResult<Item>> AddItem(Item item)
    {
      _context.Items.Add(item);
      await _context.SaveChangesAsync();
      return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
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

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(int Id)
    {
      var item = await _context.Items.FindAsync(Id);

      if (item == null){
        return NotFound();
      }

      _context.Items.Remove(item);
      await _context.SaveChangesAsync();
      return NoContent();
    }

    //Here you can add Delete All button too
  }
}