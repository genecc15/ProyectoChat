using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoEDII.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks; 

namespace ProyectoEDII.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Index()
        {
            return View();
        }

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

        public ActionResult Eliminar()
        {
            return View();
        }

        [HttpDelete] //Eliminacion Usuarios
        public async Task<ActionResult> Eliminar(string User)
        {
            var NuevaCuenta = new Usuarios();
            NuevaCuenta.User = User;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51833/api/Usuarios");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if(NuevaCuenta!=null)
                {
                    var response = await client.DeleteAsync("http://localhost:51833/api/Usurios" + NuevaCuenta.Id);
                    ViewBag.Eliminado = "Registro Eliminado";
                    return View();
                }
            }
        }
    }
}