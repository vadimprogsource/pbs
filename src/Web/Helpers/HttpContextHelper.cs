using Api;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Helpers
{
    public static class HttpContextHelper
    {
        public static IAuthSession GetSecurityToken(this HttpContext context )
        {
            UserPrincipalModel user = context.User as UserPrincipalModel;

            if (user == null)
            {
                return null;
            }

            return user.SecurityToken;
        }
    }
}
