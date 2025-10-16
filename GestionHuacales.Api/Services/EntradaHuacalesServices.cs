using System.Linq.Expressions;
using GestionHuacales.Api.Models;
using Microsoft.EntityFrameworkCore;
using GestionHuacales.Api.DAL;
using GestionHuacales.Api.Models;
using GestionHuacales.Api.DTO;

namespace GestionHuacales.Api.Services;


public class EntradaHuacalesServices(IDbContextFactory<Contexto> DbFactory)
{

    public async Task<bool> Guardar(EntradaHuacales entradaHuacales)
    {
        if (!await Existe(entradaHuacales.IdEntrada))
        {
            return await Insertar(entradaHuacales);
        }
        else
        {
            return await Modificar(entradaHuacales);
        }
    }

    private async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradaHuacales.AnyAsync(p => p.IdEntrada == id);
    }

    private async Task<bool> Insertar(EntradaHuacales entradaHuacales)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.EntradaHuacales.Add(entradaHuacales);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(EntradaHuacales entradaHuacales)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var entradaAnterior = await contexto.EntradaHuacales
            .Include(e => e.entradaHuacalesDetalle)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.IdEntrada == entradaHuacales.IdEntrada);

        if (entradaAnterior == null)
        {
            return false;
        }

        await AfectarEntradasHuacales(detalle: [.. entradaAnterior.entradaHuacalesDetalle],
                                      TipoOperacion.Resta);

        await AfectarEntradasHuacales([.. entradaHuacales.entradaHuacalesDetalle], TipoOperacion.Suma);

        contexto.EntradaHuacales.Update(entradaHuacales);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<EntradaHuacales?> Buscar(int idEntrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradaHuacales
            .Include(e => e.entradaHuacalesDetalle)
                .ThenInclude(d => d.tiposHuacales)
            .FirstOrDefaultAsync(e => e.IdEntrada == idEntrada);
    }
    public async Task<bool> Eliminar(int idEntrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var entrada = await contexto.EntradaHuacales
            .Include(e => e.entradaHuacalesDetalle)
            .FirstOrDefaultAsync(e => e.IdEntrada == idEntrada);

        if (entrada == null)
        {
            return false;
        }

        await AfectarEntradasHuacales(detalle: [.. entrada.entradaHuacalesDetalle], TipoOperacion.Resta);

        contexto.EntradaHuacalesDetalles.RemoveRange(entrada.entradaHuacalesDetalle);
        contexto.EntradaHuacales.Remove(entrada);

        var cantidad = await contexto.SaveChangesAsync();
        return cantidad > 0;
    }

    public async Task<EntradaHuacalesDto[]> Listar(Expression<Func<EntradaHuacales, bool>> criterio)
    {

        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradaHuacales
            .Where(criterio)
            .Select(h => new EntradaHuacalesDto
            {
                NombreCliente = h.NombreCliente,
                Huacales = h.entradaHuacalesDetalle.Select(d => new EntradaHuacalesDetalleDto()
                {
                    IdTipo = d.IdTipo,
                    Cantidad = d.Cantidad,
                    Precio = d.Precio
                }).ToArray()
            })
            .ToArrayAsync();
    }

    private async Task AfectarEntradasHuacales(EntradaHuacalesDetalle[] detalle, TipoOperacion tipoOperacion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        foreach (var item in detalle)
        {
            var tipoHuacal = await contexto.TiposHuacales
                .SingleAsync(t => t.IdTipo == item.IdTipo);

            if (tipoOperacion == TipoOperacion.Suma)
            {
                tipoHuacal.Existencia += item.Cantidad;
            }
            else if (tipoOperacion == TipoOperacion.Resta)
            {
                tipoHuacal.Existencia -= item.Cantidad;
            }

            await contexto.SaveChangesAsync();
        }
    }
    public enum TipoOperacion
    {
        Suma = 1,
        Resta = 2
    }
    public async Task<List<TiposHuacales>> ListarTiposHuacales()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TiposHuacales
            .AsNoTracking()
            .ToListAsync();
    }


}

