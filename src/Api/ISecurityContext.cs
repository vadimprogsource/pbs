using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public interface ISecurityContext
    {
         IAuthSession Session { get; set; }

        bool IsAdminRole { get; }
    }
}
