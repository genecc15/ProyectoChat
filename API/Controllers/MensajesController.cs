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

        //http://localhost:<port>/api/API
        [HttpGet]
        public ActionResult<List<Mensajes>> Get() =>
            _MensajesServices.Get();

        //http://localhost:<port>/api/id
        [HttpGet("{id:length(24)}", Name = "GetMensajes")]
        public ActionResult<Mensajes> Get(string id)
        {
            var Mensajes = _MensajesServices.Get(id);
            if(Mensajes==null)
            {
                return NotFound();
            }
            return Mensajes;
        }

        [HttpPost]
        public ActionResult<Mensajes> Create(Mensajes mensajes)
        {
            _MensajesServices.Create(mensajes);
            return CreatedAtRoute("GetMensajes", new { id = mensajes.Id.ToString() }, mensajes);

        }

        //http://localhost:<port>/api/API/id
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Mensajes MensajesIn)
        {
            var Mensaje = _MensajesServices.Get(id);

            if(Mensaje==null)
            {
                return NotFound();
            }
            _MensajesServices.Update(id, MensajesIn);
            return Ok();
        }

        //http://localhost:<port>/api/Mensajes/id
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var Mensaje = _MensajesServices.Get(id);

            if(Mensaje==null)
            {
                return NotFound();
            }
            _MensajesServices.Remove(Mensaje.Id);
            return NoContent();
        }
    }
}