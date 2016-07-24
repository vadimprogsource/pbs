using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api;
using PBS.Models;

namespace PBS.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {

        private readonly IAuthProvider provider;


        public AuthController(IAuthProvider provider)
        {
            this.provider = provider;
        }



        [HttpPost("SignIn")]
        public SecurityTokenModel SignIn([FromBody]LoginModel model)
        {

            IAuthSession sts = provider.LogIn(model.User, model.Password);

            if (sts != null && sts.IsAuthenticated)
            {
                return new SecurityTokenModel{ IsValid = true , Sid = sts.Sid };
            }

            return new SecurityTokenModel { IsValid = false, Sid = Guid.Empty};
        }


        [HttpPost("SignOut")]
        public IActionResult SignOut([FromBody]SecurityTokenModel model)
        {
            provider.LogOut(model.Sid);
            return new ObjectResult(new { }); 
        }
    }
}
