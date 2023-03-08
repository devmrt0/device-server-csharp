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
    public class DeviceUserController : ControllerBase
    {

        private readonly ILogger<DeviceUserController> _logger;

        public DeviceUserController(ILogger<DeviceUserController> logger)
        {
            _logger = logger;
        }

       




       

        [HttpGet]
        [Route("/api/app/device")]
        public ObjectResult GetAll()
        {
            try
            {
                DeviceUserModel user = new DeviceUserModel();
                user.Load();
                return StatusCode(StatusCodes.Status200OK, user.get2());
            }
            catch (Exception hata)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = hata.Message });

            }
        }

        [HttpGet]
        [Route("/api/app/device/{id}")]
        public ObjectResult Get(string id)
        {
            try
            {
                DeviceUserModel user = new DeviceUserModel();
                return StatusCode(StatusCodes.Status200OK, user.getDeviceId(id));
            }
            catch (Exception hata)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = hata.Message });

            }
        }


        [HttpPost]
        [Route("/api/app/device")]
        public ObjectResult Post([FromBody] DeviceUserModel.Item value)
        {
            try
            {
                DeviceUserModel user = new DeviceUserModel();
                return StatusCode(StatusCodes.Status200OK, user.Post2(value));
            }
            catch (Exception hata)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = hata.Message });

            }

        }



        [HttpPut]
        [Route("/api/app/device/{id}")]
        public ObjectResult Put(string id, [FromBody] DeviceUserModel.Item value)
        {
            try
            {
                DeviceUserModel user = new DeviceUserModel();
                return StatusCode(StatusCodes.Status200OK, user.PutById2(id, value));
            }
            catch (Exception hata)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = hata.Message });

            }

        }

        
        [HttpDelete]
        [Route("/api/app/device/{id}")]
        public ObjectResult Delete(string id)
        {
            try
            {
                DeviceUserModel user = new DeviceUserModel();
                return StatusCode(StatusCodes.Status200OK, user.DeleteById2(id));
            }
            catch (Exception hata)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = hata.Message });

            }

        }



    }
}
