using System.ComponentModel.DataAnnotations;

namespace GestionHuacales.Api.Models;

public class Tareas
{
    [Key]
    public int TareaId { get; set; }
    public string Descripcion { get; set; }
    public int Tiempo { get; set; }
}
