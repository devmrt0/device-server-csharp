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
    public class VerifyController : ControllerBase
    {

        private readonly ILogger<VerifyController> _logger;

        public VerifyController(ILogger<VerifyController> logger)
        {
            _logger = logger;
        }

        public class VerifyItem
        {
            public string id { get; set; }

            public string deviceid { get; set; }

            public string language { get; set; }

        }




        [HttpPost]
        [Route("/api/verify/uid")]
        public ObjectResult PostUid([FromBody] VerifyItem value)
        {
            try
            {
                UsersModel even = new UsersModel();
                return StatusCode(StatusCodes.Status200OK, even.verifyUid(value.id, value.deviceid, value.language));
            }
            catch (Exception hata)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = hata.Message });

            }

        }

        [HttpPost]
        [Route("/api/verify/bio")]
        public ObjectResult PostBio([FromBody] VerifyItem value)
        {
            try
            {
                UsersModel even = new UsersModel();
                //value.id,value.deviceid,value.language
                return StatusCode(StatusCodes.Status200OK, even.verifyFace());
            }
            catch (Exception hata)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = hata.Message });

            }

        }

        [HttpPost]
        [Route("/api/verify/qr")]
        public ObjectResult PostQr([FromBody] VerifyItem value)
        {
            try
            {
                UsersModel even = new UsersModel();
                //value.id,value.deviceid,value.language
                return StatusCode(StatusCodes.Status200OK, even.verifyQR());
            }
            catch (Exception hata)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = hata.Message });

            }

        }

        [HttpGet]
        [Route("/api/app/user")]
        public ObjectResult GetAll()
        {
            try
            {
                UsersModel user = new UsersModel();
                user.Load();
                return StatusCode(StatusCodes.Status200OK, user.getUserAll2());
            }
            catch (Exception hata)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = hata.Message });

            }
        }

        [HttpGet]
        [Route("/api/app/user/{id}")]
        public ObjectResult Get(string id)
        {
            try
            {
                UsersModel user = new UsersModel();
                return StatusCode(StatusCodes.Status200OK, user.getUserId(id));
            }
            catch (Exception hata)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = hata.Message });

            }
        }


        [HttpPost]
        [Route("/api/app/user")]
        public ObjectResult Post([FromBody] UsersModel.Item value)
        {
            try
            {
                UsersModel user = new UsersModel();
                return StatusCode(StatusCodes.Status200OK, user.PostUser2(value));
            }
            catch (Exception hata)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = hata.Message });

            }

        }



        [HttpPut]
        [Route("/api/app/user/{id}")]
        public ObjectResult Put(string id, [FromBody] UsersModel.Item value)
        {
            try
            {
                UsersModel user = new UsersModel();
                return StatusCode(StatusCodes.Status200OK, user.PutUserById2(id, value));
            }
            catch (Exception hata)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = hata.Message });

            }

        }

        
        [HttpDelete]
        [Route("/api/app/user/{id}")]
        public ObjectResult Delete(string id)
        {
            try
            {
                UsersModel user = new UsersModel();
                return StatusCode(StatusCodes.Status200OK, user.DeleteUserById2(id));
            }
            catch (Exception hata)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = hata.Message });

            }

        }



    }
}
