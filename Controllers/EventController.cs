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
    public class EventController : ControllerBase
    {

        private readonly ILogger<EventController> _logger;

        public EventController(ILogger<EventController> logger)
        {
            _logger = logger;
        }




        [HttpPost]
        [Route("/api/event/door/open")]
        public ObjectResult Post([FromBody] EventModel.Item value)
        {
            try
            {
                EventModel even = new EventModel();
                return StatusCode(StatusCodes.Status200OK, even.doorOpen2(value.device_id, value.event_type, value.event_dt));
            }
            catch (Exception hata)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = hata.Message });

            }

        }


        [HttpPost]
        [Route("/api/event/alarm/door/open")]
        public ObjectResult PostOpen([FromBody] EventModel.Item value)
        {
            try
            {
                EventModel even = new EventModel();
                return StatusCode(StatusCodes.Status200OK, even.doorAlarmOpen2(value.device_id, value.event_type, value.event_dt));
            }
            catch (Exception hata)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = hata.Message });

            }

        }


        [HttpPost]
        [Route("/api/event/alarm/door/close")]
        public ObjectResult PostClose([FromBody] EventModel.Item value)
        {
            try
            {
                EventModel even = new EventModel();
                return StatusCode(StatusCodes.Status200OK, even.doorAlarmClose2(value.device_id, value.event_type, value.event_dt));
            }
            catch (Exception hata)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = hata.Message });

            }

        }



    }
}
