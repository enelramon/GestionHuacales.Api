using System.ComponentModel.DataAnnotations;

namespace GestionHuacales.Api.Models;

public partial class Depositos
{
    [Key] 
    public int DepositoId { get; set; }
     
    public DateTime Fecha { get; set; }
 
    public string? Banco { get; set; }
 
    public string? Concepto { get; set; }

    public double Monto { get; set; }
}
