using GestionHuacales.Api.DAL;
using GestionHuacales.Api.DTO;
using GestionHuacales.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionHuacales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly Contexto _context;

        public MovimientosController(Contexto context)
        {
            _context = context;
        }


        // GET: api/Movimientos/5
        [HttpGet("{partidaId}")]
        public async Task<ActionResult<Movimientos[]>> GetMovimientos(int partidaId)
        {
            var movimientos = await _context.Movimientos
                .Where(m=> m.PartidaId == partidaId)
                .ToArrayAsync();

            return movimientos;
        }
        
        // POST: api/Movimientos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movimientos>> PostMovimientos(MovimientosDto movimientoDto)
        {
            var partida = await _context.Partidas.FindAsync(movimientoDto.PartidaId);
            if (partida == null)
            {
                return NotFound();
            }

            var jugador = movimientoDto.Jugador??"X";
            var movimiento = new Movimientos()
            {
                PartidaId = movimientoDto.PartidaId,
                JugadorId = jugador.Equals("X") ? partida?.Jugador1Id??0: partida?.Jugador2Id??0  ,
                PosicionFila = movimientoDto.PosicionFila,
                PosicionColumna = movimientoDto.PosicionColumna,
            };
            _context.Movimientos.Add(movimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovimientos", new { id = movimiento.MovimientoId }, movimiento);
        }

        // DELETE: api/Movimientos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimientos(int id)
        {
            var movimientos = await _context.Movimientos.FindAsync(id);
            if (movimientos == null)
            {
                return NotFound();
            }

            _context.Movimientos.Remove(movimientos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
