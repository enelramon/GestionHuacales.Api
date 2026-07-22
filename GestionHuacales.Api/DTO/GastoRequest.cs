using System;

namespace GestionHuacales.Api.Dtos;

public class GastoRequest
{
    public DateTime Fecha { get; set; }
    public required string Suplidor { get; set; } = default!;
    public required string Ncf { get; set; } = default!;
    public decimal Itbis { get; set; }
    public decimal Monto { get; set; }
}
