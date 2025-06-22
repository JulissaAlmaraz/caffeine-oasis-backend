using CaffeineOasis.API.Models; //we are using the models: MenuItem & CaffeineOasisContext
using CaffeineOasis.API.Services.Interfaces;

namespace CaffeineOasis.API.Services
{
    public class MenuItemService : IMenuItemService //implementing our interface 
    {
        private readonly CaffeineOasisContext _context; //_context: Entity Framework DbContext

        public MenuItemService(CaffeineOasisContext context) //Constructor that receives the CaffeineOasisContext and saves it in the _context field. This is the Dependeny Injection.
        {
            _context = context;
        }

        public IEnumerable<MenuItem> GetAllMenuItems() //talks to the database and returns all rows from the menu_items table. 
        {
            return _context.MenuItems.ToList();
        }

        public MenuItem? GetMenuItemById(int id) //this finds a menu item by its primary key(item_id). If no item is found, it returns null
        {
            return _context.MenuItems.Find(id);
        }
    }
}

