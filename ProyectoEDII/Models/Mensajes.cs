using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoEDII.Models
{
    public class Mensajes
    {
        public string Id { get; set; }
        public string Emisor { get; set; }
        public string Receptor { get; set; }
        public string Mensaje { get; set; }
    }
}