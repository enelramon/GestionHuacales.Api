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
    public class PartidasController : ControllerBase
    {
        private readonly Contexto _context;

        public PartidasController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Partidas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Partidas>>> GetPartidas()
        {
            return await _context.Partidas.ToListAsync();
        }

        // GET: api/Partidas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Partidas>> GetPartidas(int id)
        {
            var partidas = await _context.Partidas.FindAsync(id);

            if (partidas == null)
            {
                return NotFound();
            }

            return partidas;
        }

        // PUT: api/Partidas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPartidas(int id, Partidas partidas)
        {
            if (id != partidas.PartidaId)
            {
                return BadRequest();
            }

            _context.Entry(partidas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartidasExists(id))
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

        // POST: api/Partidas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Partidas>> PostPartidas(Partidas partidas)
        {
            _context.Partidas.Add(partidas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPartidas", new { id = partidas.PartidaId }, partidas);
        }

        // DELETE: api/Partidas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePartidas(int id)
        {
            var partidas = await _context.Partidas.FindAsync(id);
            if (partidas == null)
            {
                return NotFound();
            }

            _context.Partidas.Remove(partidas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PartidasExists(int id)
        {
            return _context.Partidas.Any(e => e.PartidaId == id);
        }
    }
}
