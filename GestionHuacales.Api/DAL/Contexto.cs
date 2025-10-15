using GestionHuacales.Api.Models;
using Microsoft.EntityFrameworkCore;
using GestionHuacales.Api.Models;

namespace GestionHuacales.Api.DAL;


public class Contexto(DbContextOptions<Contexto> options) : DbContext(options)
{
    public DbSet<EntradaHuacales> EntradaHuacales { get; set; }
    public DbSet<EntradaHuacalesDetalle> EntradaHuacalesDetalles { get; set; }

    public DbSet<TiposHuacales> TiposHuacales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TiposHuacales>().HasData(

             new List<TiposHuacales>()
            {
        new()
        {
            IdTipo = 1,
            Descripcion = "Verde",
            Existencia = 0,
        },
        new()
        {
            IdTipo = 2,
            Descripcion = "Rojo",
            Existencia = 0,
        },
         new()
        {
            IdTipo = 3,
            Descripcion = "Verde",
            Existencia = 0,
        },
          new()
        {
            IdTipo = 4,
            Descripcion = "Rojo",
            Existencia = 0,
        }
        }
    );
        base.OnModelCreating(modelBuilder);
    }
}