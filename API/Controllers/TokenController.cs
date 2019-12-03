using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using API.Models;
using Microsoft.AspNetCore.Http;

namespace API.Controllers
{
    [Route("api/JWT")]
    [ApiController]
    public class TokenController : Controller
    {
        private const string SECRET_KEY = "ThisKeyIsOnlyForPractice";
        public static readonly SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));

        public IActionResult Get([FromBody]Usuarios Json)
        {
            if (Json.Nombre == Json.Apellido)
                return new ObjectResult(GenerateToken(Json.Nombre));
            else
                return BadRequest();
        }

        private object GenerateToken(string Json)
        {

            var token = new JwtSecurityToken(
           claims: new Claim[] { new Claim(ClaimTypes.Name, Json) },
           notBefore: new DateTimeOffset(DateTime.Now).DateTime,
           expires: new DateTimeOffset(DateTime.Now.AddMinutes(60)).DateTime,
           signingCredentials: new SigningCredentials(SIGNING_KEY,
                                               SecurityAlgorithms.HmacSha256)
           );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}