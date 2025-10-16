﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GestionHuacales.Api.Models;

namespace GestionHuacales.Api.Models;

public class Partidas
{
    [Key]
    public int PartidaId { get; set; }

    public int Jugador1Id { get; set; } = 3;
    public int? Jugador2Id { get; set; } = 4;

    [Required]
    [StringLength(20)]
    public string EstadoPartida { get; set; }

    public int? GanadorId { get; set; } = 4;
    public int TurnoJugadorId { get; set; }

    [StringLength(9)]
    public string EstadoTablero { get; set; }

    public DateTime FechaInicio { get; set; } = DateTime.UtcNow;
    public DateTime? FechaFin { get; set; }

    // Propiedades de navegación
    [ForeignKey(nameof(Jugador1Id))]
    public virtual Jugadores Jugador1 { get; set; }

    [ForeignKey(nameof(Jugador2Id))]
    public virtual Jugadores Jugador2 { get; set; }

    [ForeignKey(nameof(GanadorId))]
    public virtual Jugadores Ganador { get; set; }

    [ForeignKey(nameof(TurnoJugadorId))]
    public virtual Jugadores TurnoJugador { get; set; }

    public virtual ICollection<Movimientos> Movimientos { get; set; } = new List<Movimientos>();
}