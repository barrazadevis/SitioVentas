using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SitioPersona.Models
{
    public class VentaViewModel
    {
        public Venta venta { get; set; }

        public List<Producto> Producto { get; set; }
        public List<Cliente> Cliente { get; set; }
    }
}
