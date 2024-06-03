using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Terra.Server.Data;
using Terra.Server.Models;

namespace Terra.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaysController : ControllerBase
    {
        private readonly TerraServerContext _context;

        public WaysController(TerraServerContext context)
        {
            _context = context;
        }

        // GET: api/Ways
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Way>>> GetWay()
        {
            return await _context.Way
                .Include(w => w.Cordinates)
                .Include(w => w.Checkpoints)
                .ToListAsync();
        }

        // GET: api/Ways/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Way>> GetWay(int id)
        {
            var way = await _context.Way
                .Include(w => w.Cordinates)
                .Include(w => w.Checkpoints)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (way == null)
            {
                return NotFound();
            }
            return way;
        }

        [HttpGet("{id}/midpoint")]
        public async Task<ActionResult<Dictionary<string, object>>> GetMidPoint(int id)
        {
            var way = await _context.Way
                .Include(w => w.Cordinates)
                .Include(w => w.Checkpoints)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (way == null)
            {
                return NotFound();
            }
            var responce = new Dictionary<string, object>();
            var midPoint = new Midpoint(way.Cordinates);
            responce.Add("zoom", midPoint.Zoom);
            responce.Add("midpoint", midPoint.Center);
            return responce;
        }

        // POST: api/Ways
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Way>> PostWay(Way way)
        {
            _context.Way.Add(way);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWay", new { id = way.Id }, way);
        }

        // DELETE: api/Ways/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWay(int id)
        {
            var way = await _context.Way.FindAsync(id);
            if (way == null)
            {
                return NotFound();
            }

            _context.Way.Remove(way);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WayExists(int id)
        {
            return _context.Way.Any(e => e.Id == id);
        }
    }
}
