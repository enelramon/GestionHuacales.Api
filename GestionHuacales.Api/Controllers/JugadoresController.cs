using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionHuacales.Api.DAL;
using GestionHuacales.Api.Models;

namespace GestionHuacales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JugadoresController : ControllerBase
    {
        private readonly Contexto _context;

        public JugadoresController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Jugadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jugadores>>> GetJugadores()
        {
            return await _context.Jugadores.ToListAsync();
        }

        // GET: api/Jugadores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Jugadores>> GetJugadores(int id)
        {
            var jugadores = await _context.Jugadores.FindAsync(id);

            if (jugadores == null)
            {
                return NotFound();
            }

            return jugadores;
        }

        // PUT: api/Jugadores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJugadores(int id, Jugadores jugadores)
        {
            if (id != jugadores.JugadorId)
            {
                return BadRequest();
            }

            _context.Entry(jugadores).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JugadoresExists(id))
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

        // POST: api/Jugadores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Jugadores>> PostJugadores(Jugadores jugadores)
        {
            _context.Jugadores.Add(jugadores);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJugadores", new { id = jugadores.JugadorId }, jugadores);
        }

        // DELETE: api/Jugadores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJugadores(int id)
        {
            var jugadores = await _context.Jugadores.FindAsync(id);
            if (jugadores == null)
            {
                return NotFound();
            }

            _context.Jugadores.Remove(jugadores);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JugadoresExists(int id)
        {
            return _context.Jugadores.Any(e => e.JugadorId == id);
        }
    }
}
