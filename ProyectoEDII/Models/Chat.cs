using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoEDII.Models
{
    public class Chat
    {
        public string id { get; set; }
        public string clave { get; set; }
        public List<Mensajes> Entradas { get; set; }
        public List<Mensajes> Salidas { get; set; }

        public static implicit operator List<object>(Chat v)
        {
            throw new NotImplementedException();
        }
    }
}