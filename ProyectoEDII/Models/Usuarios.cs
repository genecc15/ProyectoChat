using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoEDII.Models
{
    public class Usuarios
    {
        public string Id { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}