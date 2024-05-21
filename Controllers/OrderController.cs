using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrcaProject.Data;
using OrcaProject.Models;

namespace OrcaProject.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Order updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Order Deleted successfully." });
        }

        [HttpPost("{orderId}/close")]
        [Authorize] 
        public async Task<IActionResult> CloseOrder(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return NotFound("Order not found.");
            }

            if (order.IsClosed)
            {
                return BadRequest("Order is already closed.");
            }

            order.IsClosed = true; // Marks the order as closed
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Order Closed Successfully." }); // Returns a 204 No Content status to indicate successful closing without returning data
        }
    }

}
