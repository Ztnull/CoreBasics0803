using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JWTAuthSamples.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace JWTAuthSamples.Controllers
{
    /// <summary>
    /// JWT配置
    /// </summary> 
    [Route("api/Authorize")]
    public class AuthorizeController : Controller
    {
        private Models.JwtSettings _jwtSettings = new Models.JwtSettings();
        public AuthorizeController(IOptions<Models.JwtSettings> _jwtSettingsAccess)
        {
            _jwtSettings = _jwtSettingsAccess.Value;
        }

        public IActionResult Token(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                if (!(loginModel.User == "DreamZhang" && loginModel.Password == "123456"))
                {
                    return BadRequest();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,"DreamZhang"),
                    new Claim(ClaimTypes.Role,"Admin"),
                };

                /*
                  加密token
                */
                var token = new JwtSecurityToken(
                    _jwtSettings.Issuser,
                    _jwtSettings.Audience,
                    claims,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30)
                    );
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });


            }
            return BadRequest();
        }
    }
}