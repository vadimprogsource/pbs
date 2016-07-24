using Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Models
{
    class UserPrincipalModel : ClaimsPrincipal
    {
        public IAuthSession SecurityToken { get; }

        public UserPrincipalModel(IAuthSession securityToken)
        {
            SecurityToken = securityToken;
        }
    }

}
