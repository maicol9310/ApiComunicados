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
    [Route("api/UsuarioComunicadoes")]
    public class UsuarioComunicadoesController : ControllerBase
    {
        private readonly StoreDBContext _context;

        public UsuarioComunicadoesController(StoreDBContext context)
        {
            _context = context;
        }

        // GET: api/UsuarioComunicadoes
        [HttpGet]
        public IEnumerable<UsuarioComunicadoDTO> GetUsuarioComunicado()
        {
            return Mapper.Map<IEnumerable<UsuarioComunicadoDTO>>(_context.UsuarioComunicado.OrderByDescending(x => x.UsuarioId));
        }

        [HttpGet("Usuario/{UsuarioId}")]
        public IEnumerable<UsuarioComunicadoDTO> GetUsuario_UsuarioComunicado([FromRoute] int usuarioId)
        {
            return Mapper.Map<IEnumerable<UsuarioComunicadoDTO>>(_context.UsuarioComunicado.Where(x => x.UsuarioId == usuarioId).OrderByDescending(x => x.UsuarioId));
        }

        // GET: api/UsuarioComunicadoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUsuarioComunicado([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerOrder = await _context.UsuarioComunicado.SingleOrDefaultAsync(m => m.Id == id);

            if (customerOrder == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<UsuarioComunicadoDTO>(customerOrder));
        }

        // PUT: api/UsuarioComunicadoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioComunicado([FromRoute] int id, [FromBody] UsuarioComunicadoDTO usuarioComunicado)
        {
            usuarioComunicado.Usuario = null;
            usuarioComunicado.ExternaInterna = null;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuarioComunicado.Id)
            {
                return BadRequest();
            }

            _context.Entry(Mapper.Map<UsuarioComunicado>(usuarioComunicado)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioComunicadoExists(id))
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

        // POST: api/UsuarioComunicadoes
        [HttpPost]
        public async Task<IActionResult> PostUsuarioComunicado([FromBody] UsuarioComunicado usuarioComunicado)
        {
            usuarioComunicado.Usuario = null;
            usuarioComunicado.ExternaInterna = null;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var co = Mapper.Map<UsuarioComunicado>(usuarioComunicado);
            _context.UsuarioComunicado.Add(co);
            await _context.SaveChangesAsync();
            usuarioComunicado.Id = co.Id;

            return CreatedAtAction("GetUsuarioComunicado", new { id = co.Id }, usuarioComunicado);
        }

        // DELETE: api/UsuarioComunicadoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUsuarioComunicado([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuarioComunicado = await _context.UsuarioComunicado.SingleOrDefaultAsync(m => m.Id == id);
            if (usuarioComunicado == null)
            {
                return NotFound();
            }

            _context.UsuarioComunicado.Remove(usuarioComunicado);
            await _context.SaveChangesAsync();

            return Ok(Mapper.Map<UsuarioComunicadoDTO>(usuarioComunicado));
        }

        private bool UsuarioComunicadoExists(int id)
        {
            return _context.UsuarioComunicado.Any(e => e.Id == id);
        }
    }
}
