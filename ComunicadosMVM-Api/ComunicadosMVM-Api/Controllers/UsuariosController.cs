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
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly StoreDBContext _context;

        public UsuariosController(StoreDBContext context)
        {
            _context = context;
        }

        //GET: api/Usuario/Page/3/30
        //[HttpGet("Page/{pag}/{tam}")]
        public IEnumerable<UsuarioDTO> GetUsuario([FromRoute] int pag, [FromRoute] int tam)
        {
            var model = _context.Usuario.Skip(tam * pag - 1).Take(tam).OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
            var dto = Mapper.Map<IEnumerable<UsuarioDTO>>(model);
            return dto;
        }

        [HttpGet("")]
        public IEnumerable<UsuarioDTO> GetUsuario()
        {
            var model = _context.Usuario.OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
            var dto = Mapper.Map<IEnumerable<UsuarioDTO>>(model);
            return dto;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = await _context.Usuario.SingleOrDefaultAsync(m => m.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<UsuarioDTO>(usuario));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario([FromRoute] int id, [FromBody] UsuarioDTO usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(Mapper.Map<Usuario>(usuario)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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


        [HttpPost]
        public async Task<IActionResult> PostUsuario([FromBody] UsuarioDTO usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var map = Mapper.Map <Usuario>(usuario);

            _context.Usuario.Add(map);
            await _context.SaveChangesAsync();
            usuario.Id = map.Id;

            return CreatedAtAction("GetUsuario", new { id = map.Id }, usuario);
        }



        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] dynamic credentials)
        {
            var username = (string)credentials["username"];
            var password = (string)credentials["password"];

            var usuario = await _context.Usuario.SingleOrDefaultAsync(m => m.UserName == username && m.Password == password);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<UsuarioDTO>(usuario));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = await _context.Usuario.SingleOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return Ok(Mapper.Map<UsuarioDTO>(usuario));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }
    }
}
