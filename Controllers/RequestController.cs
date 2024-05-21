using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrcaProject.Data;
using OrcaProject.Models;

namespace OrcaProject.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RequestsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RequestsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
            return await _context.Requests.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            return request;
        }

        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Requests.Add(request);
            try
            {
                await _context.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                // Log the exception details here
                return StatusCode(500, "A problem happened while handling your request.");
            }
            return Ok(new { message = "Your Request Created Successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Your Request Deleted Successfully." });
        }
    }

}
