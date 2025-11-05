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
public class GastosController(
    Contexto context,
    IMapper mapper) : ControllerBase
{
    // GET: api/Gastos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GastoResponse>>> GetGastos()
    {
        return await context.Gastos
            .ProjectToType<GastoResponse>()
            .ToListAsync();
    }

    // GET: api/Gastos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GastoResponse>> GetGasto(int id)
    {
        var entity = await context.Gastos.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return mapper.Map<GastoResponse>(entity);
    }

    // POST: api/Gastos
    [HttpPost]
    public async Task<ActionResult<GastoResponse>> PostGasto(GastoRequest gasto)
    {
        var entity = mapper.Map<Gastos>(gasto);
        context.Gastos.Add(entity);
        await context.SaveChangesAsync();

        var response = mapper.Map<GastoResponse>(entity);
        return CreatedAtAction("GetGasto", new { id = entity.GastoId }, response);
    }

    // PUT: api/Gastos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGasto(int id, GastoRequest gasto)
    {
        var affected = await context.Gastos
            .Where(g => g.GastoId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(g => g.Fecha, gasto.Fecha)
                .SetProperty(g => g.Suplidor, gasto.Suplidor)
                .SetProperty(g => g.Ncf, gasto.Ncf)
                .SetProperty(g => g.Itbis, gasto.Itbis)
                .SetProperty(g => g.Monto, gasto.Monto)
            );

        if (affected == 0)
            return NotFound();

        return Ok();
    }
}
