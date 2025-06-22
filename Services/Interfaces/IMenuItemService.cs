using CaffeineOasis.API.Models;

namespace CaffeineOasis.API.Services.Interfaces
{
    public interface IMenuItemService
    {
        IEnumerable<MenuItem> GetAllMenuItems(); //getting all the menu items
        MenuItem? GetMenuItemById(int id); //getting a specific item by id. ? nullable, as it may not be there
    }
}
