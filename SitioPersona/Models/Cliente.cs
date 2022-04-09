using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SitioPersona.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
        }

        public int IdCliente { get; set; }

        [Required(ErrorMessage = "La cedula no puede quedar vacía")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "El Nombre no puede quedar vacío")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido no puede quedar vacío")]
        public string Apellido { get; set; }

        public string Telefono { get; set; }
    }
}
