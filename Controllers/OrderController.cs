using CaffeineOasis.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaffeineOasis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly CaffeineOasisContext _context;

        public OrderController(CaffeineOasisContext context)
        {
            _context = context;
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(o => o.OrderId == id);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAllOrders()
        {
            var items = _context.Orders.ToList();
            return Ok(items);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) { return NotFound(); }
            return Ok(order);
        }

        [HttpPost]

        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderId }, order);
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }
            _context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(order);
        }

        [HttpDelete]

        public async Task<ActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
