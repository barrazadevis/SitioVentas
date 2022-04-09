using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SitioPersona.Models
{
    public partial class VentaD
    {
        [Display(Name = "Consecutivo venta")]
        public int IdVenta { get; set; }

        [Required(ErrorMessage = "La cantidad no puede quedar vacía")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "La Cedula no puede quedar vacía")]
        [Display(Name = "Cedula")]
        public string CedulaCliente { get; set; }

        [Required(ErrorMessage = "El valor unitario no puede quedar vacío")]
        [Display(Name = "Valor unitario")]
        public decimal ValorUnitario { get; set; }

        [Required(ErrorMessage = "El valor total no puede quedar vacío")]
        [Display(Name = "Valor total")]
        public decimal ValorTotal { get; set; }

        [Required(ErrorMessage = "El codigo de producto no puede quedar vacío")]
        [Display(Name = "Codigo Producto")]
        public int? IdProducto { get; set; }
        public int? IdCliente { get; set; }

    }
}

