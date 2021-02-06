using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComunicadosMVM_Api.Models;
using ComunicadosMVM_Api.DTOs;
using AutoMapper;

namespace ComunicadosMVM_Api.Controllers
{
    [Produces("application/json")]
    [Route("api/ExternaInterna")]
    public class ExternaInternasController : ControllerBase
    {
        private readonly StoreDBContext _context;

        public ExternaInternasController(StoreDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<ExternaInternaDTO> GetExternaInterna()
        {
            return Mapper.Map<IEnumerable<ExternaInternaDTO>>(_context.ExternaInterna.OrderBy(x => x.Name));
        }

        // GET: api/OrderStatuses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExternaInterna([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var externaInterna = await _context.ExternaInterna.SingleOrDefaultAsync(m => m.Id == id);

            if (externaInterna == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ExternaInternaDTO>(externaInterna));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutExternaInterna([FromRoute] int id, [FromBody] ExternaInternaDTO externaInterna)
        {
            externaInterna.UsuarioComunicado = null;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != externaInterna.Id)
            {
                return BadRequest();
            }

            _context.Entry(Mapper.Map<ExternaInterna>(externaInterna)).State = EntityState.Modified;

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

        // POST: api/OrderStatuses
        [HttpPost]
        public async Task<IActionResult> PostExternaInterna([FromBody] ExternaInternaDTO externaInterna)
        {
            externaInterna.UsuarioComunicado = null;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var os = Mapper.Map<ExternaInterna>(externaInterna);

            _context.ExternaInterna.Add(os);
            await _context.SaveChangesAsync();
            externaInterna.Id = os.Id;

            return CreatedAtAction("GetExternaInterna", new { id = os.Id }, externaInterna);    
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExternaInterna([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var externaInterna = await _context.ExternaInterna.SingleOrDefaultAsync(m => m.Id == id);
            if (externaInterna == null)
            {
                return NotFound();
            }

            _context.ExternaInterna.Remove(externaInterna);
            await _context.SaveChangesAsync();

            return Ok(Mapper.Map<ExternaInternaDTO>(externaInterna));
        }

        private bool ExternaInternaExists(int id)
        {
            return _context.ExternaInterna.Any(e => e.Id == id);
        }
    }
}
