namespace GestionHuacales.Api.Dtos;

public class DepositoResponse
{
    public int DepositoId { get; set; }
    public DateTime Fecha { get; set; }
    public string? Banco { get; set; }
    public string? Concepto { get; set; }
    public double Monto { get; set; }
}
