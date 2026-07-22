namespace GestionHuacales.Api.Dtos;

public class TareaRequest
{
    public required string Descripcion { get; set; } = default!;
    public int Tiempo { get; set; }
}
