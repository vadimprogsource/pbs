using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace PBS.Controllers
{
    [Route("")]
    public class DefaultController : Controller
    {

        [HttpGet()]
        public IActionResult Get()
        {
            return File("~/views/index.html", "text/html");
        }


    }
}
