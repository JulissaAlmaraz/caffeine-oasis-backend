using CaffeineOasis.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CaffeineOasis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly CaffeineOasisContext _context;

        private bool MenuItemExists(int id)
        {
            return _context.MenuItems.Any(m => m.ItemId == id);
        }

        public MenuItemController(CaffeineOasisContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MenuItem>> GetAllMenuItems()
        {
            var items = _context.MenuItems.ToList();
            return Ok(items);
        }


        [HttpGet("{id}")]
        
        public async Task<ActionResult<MenuItem>> GetMenuItemById(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);

            if (menuItem == null) 
            {
                return NotFound();
            }
            return menuItem;
        }

        [HttpPost]

        public async Task<ActionResult<MenuItem>> CreateMenuItem(MenuItem menuItem)
        {
            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMenuItemById), new { id = menuItem.ItemId }, menuItem);
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateMenuItem(int id, MenuItem menuItem)
        { 
            if (id != menuItem.ItemId)
            {
                return BadRequest();
            }

            _context.Entry(menuItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try 
            {
                await _context.SaveChangesAsync();
            }
            catch (DBConcurrencyException)
            {
                if (!MenuItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete]

        public async Task<ActionResult> DeleteMenuItem(int id) 
        
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if(menuItem == null) 
            { 
                return NotFound(); 
            }

            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
