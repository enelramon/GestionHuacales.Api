using GestionHuacales.Api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionHuacales.Api.DTO
{
    public class MovimientosDto
    {
        public int PartidaId { get; set; }

        public string Jugador { get; set; } = "X";

        public int PosicionFila { get; set; }

        public int PosicionColumna { get; set; }
                
    }
}
