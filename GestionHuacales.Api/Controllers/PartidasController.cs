using GestionHuacales.Api.DAL;
using GestionHuacales.Api.DTO;
using GestionHuacales.Api.Models;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionHuacales.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PartidasController(
    Contexto context,
    IMapper mapper) : ControllerBase
{

    // GET: api/Partidas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PartidaResponse>>> GetPartidas()
    {
        return await context.Partidas
            .ProjectToType<PartidaResponse>()
            .ToListAsync();
    }

    // GET: api/Partidas/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PartidaResponse>> GetPartidas(int id)
    {
        var partidas = await context.Partidas.FindAsync(id);

        if (partidas == null)
        {
            return NotFound();
        }

        return mapper.Map<PartidaResponse>(partidas);
    }
    // POST: api/Partidas
    [HttpPost]
    public async Task<ActionResult<PartidaResponse>> PostPartidas(PartidaRequest partidaRequest)
    {
        var partidasEntity = mapper.Map<Partidas>(partidaRequest);
        partidasEntity.TurnoJugadorId = partidasEntity.Jugador1Id;
        context.Partidas.Add(partidasEntity);
        await context.SaveChangesAsync();

        var partidaResponse = mapper.Map<PartidaResponse>(partidasEntity);

        return CreatedAtAction("GetPartidas", new { id = partidasEntity.PartidaId }, partidaResponse);
    }

    // PUT: api/Partidas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPartidas(int id, PartidaRequest partidaResponse)
    {
        await context.Partidas
            .Where(p => p.PartidaId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Jugador1Id, partidaResponse.Jugador1Id)
                .SetProperty(p => p.Jugador2Id, partidaResponse.Jugador2Id)
            );

        return Ok();
    }


}
