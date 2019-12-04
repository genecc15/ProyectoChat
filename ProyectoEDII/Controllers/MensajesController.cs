using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using ProyectoEDII.Models;
using System.Text;

namespace ProyectoEDII.Controllers
{
    public class MensajesController : Controller
    {
        // GET: Mensajes
        /*public ActionResult Index()
        {
            return View();
        }*/
        [HttpGet]
        public async Task<ActionResult> Index()//string emisor)
        {
            /*if (emisor == null) { }
            else if (emisor == "") { }
            else
            {
                TempData["usuario"] = emisor;
            }*/

            /*
                 using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44326/api/Mensajes");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var response = await client.PostAsJsonAsync("Mensajes", NuevoMensaje))
                {
             * 
             */

            Mensajes m = new Mensajes();
            string urlGET = "https://localhost:44326/api/Mensajes";
            var clienteGET = new HttpClient();
            var jsonGET = await clienteGET.GetStringAsync(urlGET);
            var mensajes = JsonConvert.DeserializeObject<List<Mensajes>>(jsonGET);

            return View(mensajes);

        }

        public ActionResult Mensaje()
        {
            return View();
        }

        [HttpPost]//Conversacion
        public async Task<ActionResult> Mensaje(string Emisor, string Receptor, string Mensaje)
        {
            var NuevoMensaje = new Mensajes();
            NuevoMensaje.Emisor = Emisor;
            NuevoMensaje.Receptor = Receptor;
            NuevoMensaje.Mensaje = Mensaje;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44326/api/Mensajes");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var response = await client.PostAsJsonAsync("Mensajes", NuevoMensaje))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        ViewBag.MensajeEnviado = "Mensaje Enviado";
                        return View();
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ViewBag.NoEnviado = "Mensaje No Enviado";
                        ViewBag.MensajeEnviado = string.Empty;
                        return View("Mensaje");
                    }
                    else
                    {
                        ModelState.AddModelError("ErrorServidor", "Error");
                        return View("Mensaje");
                    }
                }
            }
        }
        #region ignorar
        /*
        [HttpGet]
        public async Task<ActionResult> Create(string usuario)
        {
            if (usuario == null) { }
            else if (usuario == "") { }
            else
            {
                ViewBag.Usuario = usuario;
            }

            Listas Mensaje = new Listas();
            string url = "https://localhost:44326/api/Usuarios";
            var clientGET = new HttpClient();
            var JsonGet = await clientGET.GetStringAsync(url);
            var usuarios = JsonConvert.DeserializeObject<List<usuario>>(JsonGet);
            List<string> Revisar = new List<string>();
            foreach(usuario item in usuarios)
            {
                Revisar.Add(item.user);
            }
            Mensaje.Usuarios = Revisar;
            return View(Mensaje);
        }

        [HttpPost]//Envio de mensajes
        public async Task<ActionResult> Create(string usuario, string contacto, string mensaje)
        {
            try
            {
                ViewBag.Usuario = usuario;
                ViewBag.Contacto = contacto;

                if(usuario!=""&& contacto!="" && mensaje!="")
                {
                    var client = new HttpClient();
                    string clave = usuario + "-" + contacto;
                    string urlchat = "https://localhost:44236/api/Mensajes" + clave;
                    try
                    {
                        var clientGet = new HttpClient();
                        string urlGet = "https://localhost:44236/api/Usuarios";
                        var JsonGet = await clientGet.GetStringAsync(urlGet);
                        var usuarios = JsonConvert.DeserializeObject<List<usuario>>(JsonGet);
                        List<string> PP = new List<string>();
                        foreach(usuario item in usuarios)
                        {
                            PP.Add(item.user);
                        }

                        var client2 = new HttpClient();
                        string user = "https://localhost:44236/api/Usuarios/" + contacto;

                        var Json2 = await client2.GetStringAsync(user);
                        var Usuario = JsonConvert.DeserializeObject<Usuarios>(Json2);

                        if(usuario==null)
                        {
                            ViewBag.Contacto = null;
                            ViewBag.Mensaje = "No existe el usuario";
                            Listas Listas = new Listas();
                            Listas.Usuarios = PP;
                            Listas.Mensajes = new List<string>();
                            return View(Listas);
                        }
                        else
                        {
                            var Json = await client.GetStringAsync(urlchat);
                            var Recibido = JsonConvert.DeserializeObject<Chat>(Json);
                            Mensajes MensajeUpdate = new Mensajes();
                            MensajeUpdate.fecha = DateTime.Now;
                            MensajeUpdate.mensaje = mensaje;

                            Listas Listas = new Listas();

                            using (var ClientUpdate = new HttpClient())
                            {
                                ClientUpdate.BaseAddress = new Uri("https://localhost:44236/");
                                Chat ChatUpdate = new Chat();
                                ChatUpdate.clave = clave;
                                ChatUpdate.id = Recibido.id;
                                ChatUpdate.Entradas = Recibido.Entradas;
                                List<Mensajes> Salidas = Recibido.Salidas;
                                if(Salidas==null)
                                {
                                    Salidas = new List<Mensajes>();
                                }
                                Salidas.Add(MensajeUpdate);

                                ChatUpdate.Salidas = Salidas;
                                string urlUpdate = "Https://localhost:44236/api/Mensajes/" + Recibido.id;
                                var JsonUpdate = await ClientUpdate.PutAsync(urlUpdate, new StringContent(
                                    new JavaScriptSerializer().Serialize(ChatUpdate), Encoding.UTF8, "application/json"));

                            }
                            using (var ClientUpdate = new HttpClient())
                            {
                                string clave2 = contacto + "-" + usuario;
                                string Mensaje2 = "Https://localhost:44236/api/Mensajes/" + clave2;
                                var Json3 = await client.GetStringAsync(Mensaje2);
                                var Recibido2 = JsonConvert.DeserializeObject<Chat>(Json3);

                                ClientUpdate.BaseAddress = new Uri("Https://localhost:44236/");
                                Chat ChatUpdate = new Chat();
                                ChatUpdate.clave = clave2;
                                ChatUpdate.id = Recibido2.id;
                                ChatUpdate.Salidas = Recibido2.Salidas;
                                List<Mensajes> Recibidos = Recibido2.Entradas;
                                if(Recibidos==null)
                                {
                                    Recibidos = new List<Mensajes>();
                                }
                                MensajeUpdate.mensaje = mensaje;
                                Recibidos.Add(MensajeUpdate);

                                ChatUpdate.Entradas = Recibidos;
                                string Update = "Https://localhost:44236/api/Mensajes/" + Recibido2.id;
                                var JsonUpdate = await ClientUpdate.PutAsync(Update, new StringContent(
                                    new JavaScriptSerializer().Serialize(ChatUpdate), Encoding.UTF8), "application/json"));
                            }
                            //Listas.Mensajes = Recibido;

                        }
                    }
                }
            }
        }*/
#endregion
        }
    }