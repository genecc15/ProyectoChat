using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoEDII.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
namespace ProyectoEDII.Controllers


{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Index()
        {
            return View();
        }
        #region registro
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]//Registro de usuarios
        public async Task<ActionResult> Create(string Nombre, string Apellido, string User, string Password)
        {
            var NuevaCuenta = new Usuarios();
            NuevaCuenta.Nombre = Nombre;
            NuevaCuenta.Apellido = Apellido;
            NuevaCuenta.User = User;
            NuevaCuenta.Password = Password;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44326/api/Usuarios");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var response = await client.PostAsJsonAsync("Usuarios", NuevaCuenta))
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        ViewBag.RegistroExitoso = "Registro Exitoso";
                        return View();
                    }
                    else if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ViewBag.Repetido = "El nombre de usuario ya existe";
                        ViewBag.RegistroExitoso = string.Empty;
                        return View("Create");
                    }
                    else
                    {
                        ModelState.AddModelError("ErrorServidor", "Error");
                        return View("Create");
                    }
                }
            }
        }

        #endregion

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public async Task<ActionResult> Login(string user, string password)
        {
            try
            {
                var client = new HttpClient();
                string usuario = "https://localhost:44326/api/Usuarios/" + user;

                var json = await client.GetStringAsync(usuario);
                var Usuario = JsonConvert.DeserializeObject<Usuarios>(json);
                if(Usuario != null)
                {
                    var client2 = new HttpClient();

                    client2.BaseAddress = new Uri("https://localhost:44326");
                    client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var token = await client2.PostAsync("api/Token", new StringContent(
                    new JavaScriptSerializer().Serialize(Usuario), Encoding.UTF8, "application/json"));
                if(token.IsSuccessStatusCode)
                {
                    ViewBag.Ingreso = "Bienvenido" + Usuario.Nombre;
                    return RedirectToAction("Create", "Usuarios");
                }
                else
                {
                    ViewBag.Denegado = "No ha podido ingresar";
                    return View();
                }
                }
            else
            {
                ViewBag.Denegado = "Usuario o Contrasenia Incorrecta";
                return View();
            }
            }
                    catch
            {
            ViewBag.Denegado = "Usuario o Contrasenia Incorreta";
            return View();
            }
        }
        public ActionResult Eliminar()
        {
            return View();
        }
        #region eliminar
        /* [HttpPost] //Eliminacion Usuarios
         public async Task<ActionResult> Eliminar(string User)
         {
             var NuevaCuenta = new Usuarios();
             NuevaCuenta.User = User;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri("https://localhost:44326/api/Usuarios/" + NuevaCuenta.Id);
                 client.DefaultRequestHeaders.Accept.Clear();
                 client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                 using (var response = await client.DeleteAsync("Usuarios", NuevaCuenta.Id))
                 {
                     if(response.StatusCode == System.Net.HttpStatusCode.Created)
                     {
                         ViewBag.RegistroExitoso = "Registro existoso";
                         return View();
                     }
                     else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                     {
                         ViewBag.IsRepetido = "El nombre de usuario ya existe!!!";
                         ViewBag.RegistroExitoso = string.Empty;
                         return View();
                     }
                     else
                     {
                         ModelState.AddModelError("ErrorServidor", "Error.");
                         return View();
                     }
                 }
             }
         }*/
        #endregion
    }
}