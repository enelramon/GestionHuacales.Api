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
public class TareasController(
    Contexto context,
    IMapper mapper) : ControllerBase
{

    // GET: api/Tareas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TareaResponse>>> GetTareas()
    {
        return await context.Tareas
            .ProjectToType<TareaResponse>()
            .ToListAsync();
    }

    // GET: api/Tareas/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TareaResponse>> GetTarea(int id)
    {
        var entity = await context.Tareas.FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        return mapper.Map<TareaResponse>(entity);
    }

    // POST: api/Tareas
    [HttpPost]
    public async Task<ActionResult<TareaResponse>> PostTarea(TareaRequest tarea)
    {
        var entity = mapper.Map<Tareas>(tarea);
        context.Tareas.Add(entity);
        await context.SaveChangesAsync();

        var response = mapper.Map<TareaResponse>(entity);
        return CreatedAtAction("GetTarea", new { id = entity.TareaId }, response);
    }

    // PUT: api/Tareas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTarea(int id, TareaRequest tarea)
    {
        var affected = await context.Tareas
            .Where(t => t.TareaId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(t => t.Descripcion, tarea.Descripcion)
                .SetProperty(t => t.Tiempo, tarea.Tiempo)
            );

        if (affected == 0)
            return NotFound();

        return Ok();
    }
}
