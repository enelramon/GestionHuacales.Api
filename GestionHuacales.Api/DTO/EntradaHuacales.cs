namespace GestionHuacales.Api.DTO;

public class EntradaHuacalesDto
{
    public string NombreCliente { get; set; }
    public EntradaHuacalesDetalleDto[] Huacales { get; set; } = [];
}
