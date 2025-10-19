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
        public async Task<ActionResult<MovimientosDto[]>> GetMovimientos(int partidaId)
        {

            var partida = await _context.Partidas.FindAsync(partidaId);
            if (partida == null)
            {
                return NotFound($"No se Encontro la partida con el id:{partidaId}");
            }

            var movimientos = await _context.Movimientos
                .Where(m => m.PartidaId == partidaId)
                .ToArrayAsync();

            var movimientosDto = movimientos.Select(m => new MovimientosDto
            {
                PartidaId = m.PartidaId,
                Jugador = m.JugadorId == partida.Jugador1Id ? "X" : "O",
                PosicionFila = m.PosicionFila,
                PosicionColumna = m.PosicionColumna
            }).ToArray();

            return movimientosDto;
        }

        // POST: api/Movimientos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movimientos>> PostMovimientos(MovimientosDto movimientoDto)
        {
            var partida = await _context.Partidas.FindAsync(movimientoDto.PartidaId);
            if (partida == null)
            {
                return NotFound($"No se Encontro la partida con el id:{movimientoDto.PartidaId}");
            }
            if (partida.EstadoPartida.Contains("Finalizada"))
            {
                return BadRequest("La partida ya ha finalizado. No se pueden agregar mas movimientos.");
            }

            var jugador = movimientoDto.Jugador ?? "X";
            var jugadorId = jugador.Equals("X") ? partida.Jugador1Id : partida.Jugador2Id;

            #region Validar Movimiento

            var esSuTurno = partida.TurnoJugadorId == (jugador.Equals("X") ? partida.Jugador1Id : partida.Jugador2Id);
            if (!esSuTurno)
            {
                return BadRequest("No es el turno de este jugador.");
            }

            var existeMovimiento = await _context.Movimientos
               .AnyAsync(m => m.PartidaId == movimientoDto.PartidaId &&
                              m.PosicionFila == movimientoDto.PosicionFila &&
                              m.PosicionColumna == movimientoDto.PosicionColumna);

            if (existeMovimiento)
                return BadRequest($"La posicion ya esta ocupada por otro movimiento.");

            var totalMovimientos = await _context.Movimientos.CountAsync(m => m.PartidaId == movimientoDto.PartidaId);

            if (totalMovimientos >= 9)
            {
                return BadRequest("La partida ya ha alcanzado el maximo de movimientos.");
            }

            #endregion

            #region Crear Movimiento

            var movimiento = new Movimientos()
            {
                PartidaId = movimientoDto.PartidaId,
                JugadorId = jugador.Equals("X") ? partida?.Jugador1Id ?? 0 : partida?.Jugador2Id ?? 0,
                PosicionFila = movimientoDto.PosicionFila,
                PosicionColumna = movimientoDto.PosicionColumna,
            };
            _context.Movimientos.Add(movimiento);
            await _context.SaveChangesAsync();
            #endregion

            #region Comprobaciones y Acciones

            // Cambiar Turno
            partida.TurnoJugadorId = jugador.Equals("X") ? partida.Jugador2Id ?? 0 : partida.Jugador1Id;
            await _context.SaveChangesAsync();

            // Comprobar si hay ganador o eMpate
            var movimientosPartida = await _context.Movimientos
                .Where(m => m.PartidaId == movimientoDto.PartidaId)
                .ToListAsync();


            var tablero = new int[3, 3];
            foreach (var mov in movimientosPartida)
            {
                var jugadorNum = mov.JugadorId == partida.Jugador1Id ? 1 : 2;
                tablero[mov.PosicionFila, mov.PosicionColumna] = jugadorNum;
            }

            bool ComprobarGanador(int jugadorNum)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (tablero[i, 0] == jugadorNum && tablero[i, 1] == jugadorNum && tablero[i, 2] == jugadorNum)
                        return true;
                    if (tablero[0, i] == jugadorNum && tablero[1, i] == jugadorNum && tablero[2, i] == jugadorNum)
                        return true;
                }
                if (tablero[0, 0] == jugadorNum && tablero[1, 1] == jugadorNum && tablero[2, 2] == jugadorNum)
                    return true;
                if (tablero[0, 2] == jugadorNum && tablero[1, 1] == jugadorNum && tablero[2, 0] == jugadorNum)
                    return true;
                return false;
            }

            if (ComprobarGanador(1))
            {
                partida.EstadoPartida = "Finalizada";
                partida.GanadorId = partida.Jugador1Id;
                partida.FechaFin = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            else if (ComprobarGanador(2))
            {
                partida.EstadoPartida = "Finalizada";
                partida.GanadorId = partida.Jugador2Id;
                partida.FechaFin = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            else
            {
                partida.EstadoPartida = "Finalizada";
                partida.FechaFin = DateTime.UtcNow;
            }

            #endregion

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
