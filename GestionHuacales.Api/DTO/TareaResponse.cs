namespace GestionHuacales.Api.Dtos;

public class TareaResponse
{
    public int TareaId { get; set; }
    public required string Descripcion { get; set; } = default!;
    public int Tiempo { get; set; }
}
