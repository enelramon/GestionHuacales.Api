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
public class DepositosController(
    Contexto context,
    IMapper mapper) : ControllerBase
{
    // GET: api/Depositos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepositoResponse>>> GetDepositos()
    {
        return await context.Depositos
            .ProjectToType<DepositoResponse>()
            .ToListAsync();
    }

    // GET: api/Depositos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DepositoResponse>> GetDeposito(int id)
    {
        var entity = await context.Depositos.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return mapper.Map<DepositoResponse>(entity);
    }

    // POST: api/Depositos
    [HttpPost]
    public async Task<ActionResult<DepositoResponse>> PostDeposito(DepositoRequest deposito)
    {
        var entity = mapper.Map<Depositos>(deposito);
        context.Depositos.Add(entity);
        await context.SaveChangesAsync();

        var response = mapper.Map<DepositoResponse>(entity);
        return CreatedAtAction("GetDeposito", new { id = entity.DepositoId }, response);
    }

    // PUT: api/Depositos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDeposito(int id, DepositoRequest deposito)
    {
        var affected = await context.Depositos
            .Where(d => d.DepositoId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(d => d.Fecha, deposito.Fecha)
                .SetProperty(d => d.Banco, deposito.Banco)
                .SetProperty(d => d.Concepto, deposito.Concepto)
                .SetProperty(d => d.Monto, deposito.Monto)
            );

        if (affected == 0)
            return NotFound();

        return Ok();
    }
}
