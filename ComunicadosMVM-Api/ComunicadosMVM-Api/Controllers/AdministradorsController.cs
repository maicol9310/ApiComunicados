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
    [Route("api/Employees")]
    public class AdministradorsController : ControllerBase
    {
        private readonly StoreDBContext _context;

        public AdministradorsController(StoreDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<AdministradorDTO> GetAdministrador()
        {
            return Mapper.Map<IEnumerable<AdministradorDTO>>(_context.Administrador.OrderBy(x => x.LastName).ThenBy(x => x.FirstName));
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdministrador([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var administrador = await _context.Administrador.SingleOrDefaultAsync(m => m.Id == id);

            if (administrador == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<AdministradorDTO>(administrador));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] dynamic credentials)
        {
            var username = (string)credentials["username"];
            var password = (string)credentials["password"];

            var administrador = await _context.Administrador.SingleOrDefaultAsync(m => m.UserName == username && m.Password == password);

            if (administrador == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<AdministradorDTO>(administrador));
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdministrador([FromRoute] int id, [FromBody] AdministradorDTO administrador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != administrador.Id)
            {
                return BadRequest();
            }

            _context.Entry(Mapper.Map<Administrador>(administrador)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministradorExists(id))
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
        public async Task<IActionResult> PostAdministrador([FromBody] AdministradorDTO administrador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var e = Mapper.Map<Administrador>(administrador);
            _context.Administrador.Add(e);
            await _context.SaveChangesAsync();
            administrador.Id = e.Id;

            return CreatedAtAction("GetAdministrado", new { id = e.Id }, administrador);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdministrador([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var administrador = await _context.Administrador.SingleOrDefaultAsync(m => m.Id == id);
            if (administrador == null)
            {
                return NotFound();
            }

            _context.Administrador.Remove(administrador);
            await _context.SaveChangesAsync();

            return Ok(Mapper.Map<AdministradorDTO>(administrador));
        }


        private bool AdministradorExists(int id)
        {
            return _context.Administrador.Any(e => e.Id == id);
        }
    }
}
