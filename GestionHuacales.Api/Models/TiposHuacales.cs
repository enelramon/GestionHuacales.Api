using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionHuacales.Api.Models;


public class TiposHuacales
{
    [Key]
    public int IdTipo { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "En este campo solo se permiten letras. ")]
    public string Descripcion {get; set; }


    [Required(ErrorMessage = "Este campo es obligatorio")]
    [Range(1, double.MaxValue, ErrorMessage = "Debe introducir una cantidad valida")]
    public int Existencia { get; set; }

    [InverseProperty("tiposHuacales")]
    public virtual ICollection<EntradaHuacalesDetalle> EntradaHuacalesDetalle { get; set; } = [];


}


