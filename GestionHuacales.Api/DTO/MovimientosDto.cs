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
