using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionHuacales.Api.Models;

public class Jugadores
{
    [Key]
    public int JugadorId { get; set; }

    #region "Otros Campos"

    [Required(ErrorMessage = "Nombre del jugador obligatorio")]
    [StringLength(50)]
    public string Nombres { get; set; }

    [Required(ErrorMessage = "Email del jugador obligatorio")]
    [StringLength(100)]
    public string Email { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public int Victorias { get; set; } = 0;
    public int Derrotas { get; set; } = 0;
    public int Empates { get; set; } = 0;

    // Propiedades de navegación
    [InverseProperty(nameof(Partidas.Jugador1))]
    public virtual ICollection<Partidas> PartidasComoJugador1 { get; set; } = new List<Partidas>();

    [InverseProperty(nameof(Partidas.Jugador2))]
    public virtual ICollection<Partidas> PartidasComoJugador2 { get; set; } = new List<Partidas>();

    [InverseProperty(nameof(Partidas.Ganador))]
    public virtual ICollection<Partidas> PartidasGanadas { get; set; } = new List<Partidas>();
    #endregion

    [InverseProperty(nameof(Models.Movimientos.Jugador))]
    public virtual ICollection<Movimientos> Movimientos { get; set; } = new List<Movimientos>();
}