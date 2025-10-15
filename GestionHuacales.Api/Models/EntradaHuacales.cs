
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using GestionHuacales.Api.DAL;
using GestionHuacales.Api.Models;

namespace GestionHuacales.Api.Models;


public class EntradaHuacales
{
    [Key]
    public int IdEntrada { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "En este campo solo se permiten letras. ")]
    public string NombreCliente { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public DateTime Fecha { get; set; } = DateTime.Now;

    public decimal Importe { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }

    [InverseProperty("entradaHuacales")]
    public virtual ICollection<EntradaHuacalesDetalle> entradaHuacalesDetalle { get; set; } = [];


}

