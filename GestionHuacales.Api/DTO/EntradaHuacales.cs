using GestionHuacales.Api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionHuacales.Api.DTO;

public class EntradaHuacalesDto
{
    public string NombreCliente { get; set; }
    public EntradaHuacalesDetalleDto[] Huacales { get; set; } = [];
}