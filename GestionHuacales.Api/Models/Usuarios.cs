using System.ComponentModel.DataAnnotations;

namespace GestionHuacales.Api.Models;

public class Usuarios
{
    [Key]
    public int UsuarioId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}
