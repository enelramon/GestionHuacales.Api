using GestionHuacales.Api.DAL;
using GestionHuacales.Api.Dtos;
using GestionHuacales.Api.Models;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionHuacales.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class JugadoresController(
    Contexto context,
    IMapper mapper) : ControllerBase
{

    // GET: api/Jugadores
    [HttpGet]
    public async Task<ActionResult<IEnumerable<JugadorResponse>>> GetJugadores()
    {
        return await context.Jugadores
            .ProjectToType<JugadorResponse>()
            .ToListAsync();
    }

    // GET: api/Jugadores/5
    [HttpGet("{id}")]
    public async Task<ActionResult<JugadorResponse>> GetJugador(int id)
    {
        var jugador = await context.Jugadores.FindAsync(id);

        if (jugador == null)
        {
            return NotFound();
        }

        return mapper.Map<JugadorResponse>(jugador);
    }



    // POST: api/Jugadores 
    [HttpPost]
    public async Task<ActionResult<JugadorResponse>> PostJugadores(JugadorRequest jugador)
    {
        var jugadorEntity = mapper.Map<Jugadores>(jugador);
        context.Jugadores.Add(jugadorEntity);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetJugadores", new { id = jugadorEntity.JugadorId }, jugador);
    }

    // PUT: api/Jugadores/5 
    [HttpPut("{id}")]
    public async Task<IActionResult> PutJugadores(int id, JugadorRequest jugador)
    {
        await context.Jugadores
            .Where(j => j.JugadorId == id)
            .ExecuteUpdateAsync(s => s
                    .SetProperty(j => j.Nombres, jugador.Nombres)
                    .SetProperty(j => j.Email, jugador.Email)
            );

        return Ok();
    }

}
