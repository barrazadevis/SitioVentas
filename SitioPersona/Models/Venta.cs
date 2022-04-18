using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SitioPersona.Models
{
    public partial class Venta
    {
        [Display(Name = "Numero de venta")]
        public int IdVenta { get; set; }

        [Required(ErrorMessage = "La cantidad no puede quedar vacía")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El valor unitario no puede quedar vacío")]
        [Display(Name = "Valor unitario")]
        public decimal ValorUnitario { get; set; }

        [Required(ErrorMessage = "El valor total no puede quedar vacío")]
        [Display(Name = "Valor total")]
        public decimal ValorTotal { get; set; }
        public int? IdProducto { get; set; }
        public int? IdCliente { get; set; }

    }
}

