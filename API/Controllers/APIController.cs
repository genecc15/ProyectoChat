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

        public UsuariosController(UsuariosServices usuariosServices)
        {
            _UsuariosService = usuariosServices;
        }


    }
}