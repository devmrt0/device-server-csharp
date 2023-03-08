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
    [Route("[controller]")]
    public class ScreenController : ControllerBase
    {

        private readonly ILogger<ScreenController> _logger;

        public ScreenController(ILogger<ScreenController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        [Route("/api/app/screen")]
        public ObjectResult GetAll()
        {
            try
            {
                ScreenModel screen = new ScreenModel();
                screen.Load();
                return StatusCode(StatusCodes.Status200OK, screen.getScreen2());
            }
            catch (Exception hata)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = hata.Message });

            }
        }

        [HttpGet]
        [Route("/api/app/screen/{id}")]
        public ObjectResult Get(int id)
        {
            try
            {
                ScreenModel screen = new ScreenModel();
                return StatusCode(StatusCodes.Status200OK, screen.getScreenId(id));
            }
            catch (Exception hata)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = hata.Message });

            }
        }

        [HttpPut]
        [Route("/api/app/screen/{id}")]
        public ObjectResult Put(int id, [FromBody] ScreenModel.Item value)
        {
            try
            {
                ScreenModel screen = new ScreenModel();
                return StatusCode(StatusCodes.Status200OK, screen.PutScreenById2(id, value));
            }
            catch (Exception hata)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = hata.Message });

            }

        }



    }
}
