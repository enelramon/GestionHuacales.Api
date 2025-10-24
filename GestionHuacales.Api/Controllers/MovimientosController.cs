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
        [EndpointDescription("Obtiene los movimientos de una partida especifica.")]
        public async Task<ActionResult<MovimientosResponse[]>> GetMovimientos(int partidaId)
        {

            var partida = await _context.Partidas.FindAsync(partidaId);
            if (partida == null)
            {
                return NotFound($"No se Encontro la partida con el id:{partidaId}");
            }

            var movimientos = await _context.Movimientos
                .Where(m => m.PartidaId == partidaId)
                .ToArrayAsync();

            var movimientosDto = movimientos.Select(m => new MovimientosResponse
            {
                MovimientoId = m.MovimientoId,
                Jugador = m.JugadorId == partida.Jugador1Id ? "X" : "O",
                PosicionFila = m.PosicionFila,
                PosicionColumna = m.PosicionColumna
            }).ToArray();

            return movimientosDto;
        }

        // POST: api/Movimientos
        [HttpPost]
        public async Task<ActionResult> PostMovimientos(MovimientosRequest movimiento)
        {
            var partida = await _context.Partidas
                .FirstOrDefaultAsync(m => m.PartidaId == movimiento.PartidaId);
            if (partida == null)
            {
                return NotFound();
            }

            // Validar que las posiciones estén dentro del rango del tablero (0 a 2)
            if (movimiento.PosicionFila < 0 || movimiento.PosicionFila > 2 ||
                movimiento.PosicionColumna < 0 || movimiento.PosicionColumna > 2)
            {
                return BadRequest("Las coordenadas deben estar entre 0 y 2.");
            }

            // Validar que la posición no esté ya ocupada
            bool posicionOcupada = await _context.Movimientos
                .AnyAsync(m => m.PartidaId == movimiento.PartidaId &&
                       m.PosicionFila == movimiento.PosicionFila &&
                       m.PosicionColumna == movimiento.PosicionColumna);

            if (posicionOcupada)
            {
                return BadRequest($"La posición ({movimiento.PosicionFila}, {movimiento.PosicionColumna}) ya está ocupada.");
            }

            // Validar que el jugador pertenezca a la partida
            if (movimiento.Jugador == "X" && partida.Jugador1Id == 0)
            {
                return BadRequest("La partida no tiene un jugador X asignado.");
            }

            if (movimiento.Jugador == "O" && (partida.Jugador2Id == null || partida.Jugador2Id == 0))
            {
                return BadRequest("La partida no tiene un jugador O asignado.");
            }

            // Validar que no hayan dos movimientos seguidos del mismo jugador
            var ultimoMovimiento = await _context.Movimientos
                .Where(m => m.PartidaId == movimiento.PartidaId)
                .OrderByDescending(m => m.MovimientoId)
                .FirstOrDefaultAsync();

            if (ultimoMovimiento != null)
            {
                var ultimoJugador = ultimoMovimiento.JugadorId == partida.Jugador1Id ? "X" : "O";
                if (ultimoJugador == movimiento.Jugador)
                {
                    return BadRequest($"No es el turno del jugador {movimiento.Jugador}.");
                }
            }

            var jugador = movimiento.Jugador;
            var movimientoEntity = new Movimientos()
            {
                PartidaId = movimiento.PartidaId,
                JugadorId = jugador.Equals("X") ? partida.Jugador1Id : partida.Jugador2Id ?? 0,
                PosicionFila = movimiento.PosicionFila,
                PosicionColumna = movimiento.PosicionColumna,
            };
            _context.Movimientos.Add(movimientoEntity);
            await _context.SaveChangesAsync();

            return Ok();
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
