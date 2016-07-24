using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public interface IAuthSession
    {
        Guid Sid { get; }
        Guid Uid { get; }
        DateTime Expired   { get; }
        DateTime CreatedOn { get; }

        bool IsAuthenticated { get; }

    }
}
