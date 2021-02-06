using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComunicadosMVM_Api.Models;

namespace ComunicadosMVM_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternaInternasController : ControllerBase
    {
        private readonly StoreDBContext _context;

        public ExternaInternasController(StoreDBContext context)
        {
            _context = context;
        }

        // GET: api/ExternaInternas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExternaInterna>>> GetExternaInterna()
        {
            return await _context.ExternaInterna.ToListAsync();
        }

        // GET: api/ExternaInternas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExternaInterna>> GetExternaInterna(int id)
        {
            var externaInterna = await _context.ExternaInterna.FindAsync(id);

            if (externaInterna == null)
            {
                return NotFound();
            }

            return externaInterna;
        }

        // PUT: api/ExternaInternas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExternaInterna(int id, ExternaInterna externaInterna)
        {
            if (id != externaInterna.Id)
            {
                return BadRequest();
            }

            _context.Entry(externaInterna).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExternaInternaExists(id))
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

        // POST: api/ExternaInternas
        [HttpPost]
        public async Task<ActionResult<ExternaInterna>> PostExternaInterna(ExternaInterna externaInterna)
        {
            _context.ExternaInterna.Add(externaInterna);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExternaInterna", new { id = externaInterna.Id }, externaInterna);
        }

        // DELETE: api/ExternaInternas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExternaInterna>> DeleteExternaInterna(int id)
        {
            var externaInterna = await _context.ExternaInterna.FindAsync(id);
            if (externaInterna == null)
            {
                return NotFound();
            }

            _context.ExternaInterna.Remove(externaInterna);
            await _context.SaveChangesAsync();

            return externaInterna;
        }

        private bool ExternaInternaExists(int id)
        {
            return _context.ExternaInterna.Any(e => e.Id == id);
        }
    }
}
