using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text;

namespace device_server_c_.Controllers
{
    [ApiController]
    [Route("controller")]
    public class AuthController : ControllerBase
    {



        private readonly ILogger<AuthController> _logger;

        public class UserLogin
        {
            public string jwtdeviceid { get; set; }
            public string jwtpassword { get; set; }
            public string jwtlanguage { get; set; }
        }


        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        public object getJwtClaims(string jwtdeviceid, string jwtpassword, string jwtlanguage)
        {
            var deviceid = jwtdeviceid;
            var password = jwtpassword;
            var language = jwtlanguage;
            if (Common.IsEmptyStr(deviceid) == false && Common.IsEmptyStr(password) == false && Common.IsEmptyStr(language) == false)
            {

                return new { error = false, deviceid = deviceid, password = password, language = language };
            }
            else
            {

                return new { error = true };
            }
        }

        public string GenerateToken(string deviceid, string password)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Your-Secret-Auth-Key")); // key must be 128 bit string
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,deviceid),
                new Claim(ClaimTypes.Role,password)
            };
            var token = new JwtSecurityToken(deviceid,
                password,
                claims,
                expires: DateTime.Now.AddMinutes(1000005),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),

            })
            .ToArray();
        }

        [HttpPost]
        [Route("/login")]
        public ActionResult Login(UserLogin user)
        {
            try
            {
                dynamic result = getJwtClaims(user.jwtdeviceid, user.jwtpassword, user.jwtlanguage);
                if (result.error == true)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new { message = "deviceid,password,language required.must be in Header or Body." });
                }
                DeviceUserModel myuser = new DeviceUserModel();
                myuser.Load();
                var auth = myuser.authDevice(result.deviceid, result.password);
                if (auth.id == "")
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new { message = "Forbidden" });
                }

                var token = GenerateToken(result.deviceid, result.password);

                return Ok(new { token = token });
            }
            catch (Exception hata)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = hata.Message });

            }


        }


    }
}
