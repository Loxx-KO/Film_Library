using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API.Controllers
{
    public class ProbeController : ApiController
    {
        [Route("~/")]
        public IHttpActionResult Get()
        {
            return Ok("Ok");
        }
    }
}