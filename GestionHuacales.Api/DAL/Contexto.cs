using GestionHuacales.Api.Models;
using GestionHuacales.Api.Models;
using Microsoft.EntityFrameworkCore;


namespace GestionHuacales.Api.DAL;


public class Contexto(DbContextOptions<Contexto> options) : DbContext(options)
{
    public DbSet<EntradaHuacales> EntradaHuacales { get; set; }
    public DbSet<EntradaHuacalesDetalle> EntradaHuacalesDetalles { get; set; }
    public DbSet<TiposHuacales> TiposHuacales { get; set; }
    public DbSet<Jugadores> Jugadores { get; set; }
    public DbSet<Partidas> Partidas { get; set; }
    public DbSet<Movimientos> Movimientos { get; set; }
    public DbSet<Tareas> Tareas { get; set; }
    public DbSet<Gastos> Gastos { get; set; }
    public DbSet<Depositos> Depositos { get; set; }
    public DbSet<Usuarios> Usuarios { get; set; }

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

        modelBuilder.Entity<Partidas>()
            .HasOne(p => p.Jugador1)
            .WithMany(j => j.PartidasComoJugador1)
            .HasForeignKey(p => p.Jugador1Id)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Partidas>()
            .HasOne(p => p.Jugador2)
            .WithMany(j => j.PartidasComoJugador2)
            .HasForeignKey(p => p.Jugador2Id)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Partidas>()
            .HasOne(p => p.Ganador)
            .WithMany(j => j.PartidasGanadas)
            .HasForeignKey(p => p.GanadorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Partidas>()
            .HasOne(p => p.TurnoJugador)
            .WithMany()
            .HasForeignKey(p => p.TurnoJugadorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Movimientos>()
            .HasOne(m => m.Jugador)
            .WithMany(j => j.Movimientos)
            .HasForeignKey(m => m.JugadorId)
            .OnDelete(DeleteBehavior.Restrict);


        base.OnModelCreating(modelBuilder);
    }
}