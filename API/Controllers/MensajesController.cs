using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensajesController : ControllerBase
    {
        private readonly MensajesServices _MensajesServices;

        public MensajesController(MensajesServices mensajesServices)
        {
            _MensajesServices = mensajesServices;
        }

        //http://localhost:<port>/api/Mensajes
        [HttpGet]
        public ActionResult<List<Chat>> Get() =>
            _MensajesServices.Get();

        //http://localhost:<port>/api/Mensajes/5
        [HttpGet("{llave}", Name = "GetMensajes")]
        public ActionResult<Chat> Get(string id)
        {
            var Mensajes = _MensajesServices.Get(id);
            if(Mensajes==null)
            {
                return NotFound();
            }
            return Mensajes;
        }
        //http://localhost:<port>/api/Mensajes
        [HttpPost]
        public ActionResult<Chat> Create(Chat mensajes)
        {
            _MensajesServices.Create(mensajes);
            return CreatedAtRoute("GetMensajes", new { id = mensajes.id.ToString() }, mensajes);

        }

        //http://localhost:<port>/api/Mensajes/5
        [HttpPut("{id}")]
        public IActionResult Update(string id, Chat MensajesIn)
        {
            var Mensaje = _MensajesServices.Get(id);

            if(Mensaje==null)
            {
                return NotFound();
            }
            _MensajesServices.Update(id, MensajesIn);
            return Ok();
        }

        //http://localhost:<port>/api/ApiWithActions/5
        [HttpDelete("{llave}")]
        public IActionResult Delete(string id)
        {
            var Mensaje = _MensajesServices.Get(id);

            if(Mensaje==null)
            {
                return NotFound();
            }
            _MensajesServices.Remove(Mensaje.id);
            return NoContent();
        }
    }
}