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
    public async Task<ActionResult<IEnumerable<PartidasDto>>> GetPartidas()
    {
        return await context.Partidas
            .ProjectToType<PartidasDto>()
            .ToListAsync();
    }

    // GET: api/Partidas/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PartidasDto>> GetPartidas(int id)
    {
        var partidas = await context.Partidas.FindAsync(id);

        if (partidas == null)
        {
            return NotFound();
        }

        return mapper.Map<PartidasDto>(partidas);
    }

    // PUT: api/Partidas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPartidas(int id, PartidasDto partidasDto)
    {
        await context.Partidas
            .Where(p => p.PartidaId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Jugador1Id, partidasDto.Jugador1Id)
                .SetProperty(p => p.Jugador2Id, partidasDto.Jugador2Id)
            );

        return Ok();
    }

    // POST: api/Partidas
    [HttpPost]
    public async Task<ActionResult<PartidasDto>> PostPartidas(PartidasDto partidasDto)
    {
        var partidasEntity = mapper.Map<Partidas>(partidasDto);
        partidasEntity.EstadoPartida = "";
        context.Partidas.Add(partidasEntity);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetPartidas", new { id = partidasEntity.PartidaId }, partidasDto);
    }
}
