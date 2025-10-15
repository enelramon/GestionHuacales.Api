using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using GestionHuacales.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using GestionHuacales.Api.DAL;

namespace GestionHuacales.Api.Models;

public class EntradaHuacalesDetalle
    {
        [Key]

        public int IdDetalle { get; set; }
        public int IdEntrada { get; set; }
        public int IdTipo { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe introducir una cantidad valida")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe introducir un monto valido")]
        public decimal Precio { get; set; }

        [ForeignKey("IdEntrada")]
        [InverseProperty("entradaHuacalesDetalle")]
        public virtual EntradaHuacales? entradaHuacales { get; set; } = null;

      
        [ForeignKey("IdTipo")]
        [InverseProperty("EntradaHuacalesDetalle")]

        public virtual TiposHuacales tiposHuacales { get; set; }


    }



    

