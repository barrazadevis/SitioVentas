using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SitioPersona.Models
{
    public partial class Producto
    {
        public Producto()
        {
        }

        [Display (Name = "Codigo de producto")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El nombre no puede quedar vacío")]
        public string Nombre { get; set; }

        [Required]
        [Display (Name = "Valor unitario")]
        public decimal ValorUnitario { get; set; }

    }
}
