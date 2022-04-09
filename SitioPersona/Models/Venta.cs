using System;
using System.Collections.Generic;

namespace SitioPersona.Models
{
    public partial class Venta
    {
        public int IdVenta { get; set; }
        public int Cantidad { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
        public int? IdProducto { get; set; }
        public int? IdCliente { get; set; }

    }
}

