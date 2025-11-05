using System.ComponentModel.DataAnnotations;

namespace GestionHuacales.Api.Models;

public class Gastos
{
    [Key]
    public int GastoId { get; set; }
    public DateTime Fecha { get; set; }
    public string Suplidor { get; set; }
    public string Ncf { get; set; }
    public decimal Itbis { get; set; }
    public decimal Monto { get; set; }

}