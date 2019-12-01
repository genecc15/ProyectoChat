using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly UsuariosServices _UsuariosService;

        public APIController(UsuariosServices usuariosServices)
        {
            _UsuariosService = usuariosServices;
        }

        //http://localhost:<port>/API/Usuarios
        [HttpGet]
        public ActionResult<List<Usuarios>> Get() =>
            _UsuariosService.Get();

        //http://localhost:<port>/API/Usuarios/id
        [HttpGet("{id: length(24)", Name = "GetUsuarios")]
        public ActionResult<Usuarios> Get(string id)
        {
            var Usuarios = _UsuariosService.Get(id);
            if(Usuarios==null)
            {
                return NotFound();
            }
            return Usuarios;
        }
        [HttpPost]
        public ActionResult<Usuarios> Create(Usuarios usuarios)
        {
            _UsuariosService.Create(usuarios);
            return CreatedAtRoute("GetUsuarios", new { id = usuarios.Id.ToString()}, usuarios);
                
        }

        //http://localhost<port>/API/Usuarios/id

        [HttpPut("{id: length(24)}")]

        public IActionResult Update(string id, Usuarios UsuariosIn)
        {
            var Usuario = _UsuariosService.Get(id);

            if(Usuario==null)
            {
                return NotFound();
            }

            _UsuariosService.Update(id, UsuariosIn);
            return Ok();
        }
        //http://localhost<port>/API/Usuarios/id
        [HttpDelete("{id: length(24)}")]
        public IActionResult Delete(string id)
        {
            var Usuario = _UsuariosService.Get(id);

            if(Usuario==null)
            {
                return NotFound();
            }
            _UsuariosService.Remove(Usuario.Id);
            return NoContent();
        }
    }
}