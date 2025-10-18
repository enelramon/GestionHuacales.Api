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
    public async Task<ActionResult<IEnumerable<JugadorDto>>> GetJugadores()
    {
        return await context.Jugadores
            .ProjectToType<JugadorDto>()
            .ToListAsync();
    }

    // GET: api/Jugadores/5
    [HttpGet("{id}")]
    public async Task<ActionResult<JugadorDto>> GetJugador(int id)
    {
        var jugador = await context.Jugadores.FindAsync(id);

        if (jugador == null)
        {
            return NotFound();
        }

        return mapper.Map<JugadorDto>(jugador);
    }



    // POST: api/Jugadores 
    [HttpPost]
    public async Task<ActionResult<JugadorDto>> PostJugadores(JugadorDto jugadorDto)
    {
        var jugadorEntity = mapper.Map<Jugadores>(jugadorDto);
        context.Jugadores.Add(jugadorEntity);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetJugadores", new { id = jugadorEntity.JugadorId }, jugadorDto);
    }

    // PUT: api/Jugadores/5 
    [HttpPut("{id}")]
    public async Task<IActionResult> PutJugadores(int id, JugadorDto jugadorDto)
    {
        await context.Jugadores
            .Where(j => j.JugadorId == id)
            .ExecuteUpdateAsync(s => s
                    .SetProperty(j => j.Nombres, jugadorDto.Nombres)  // Updates the Nombres column
                    .SetProperty(j => j.Email, jugadorDto.Email)      // Updates the Email column
            );

        return Ok();
    }

}
