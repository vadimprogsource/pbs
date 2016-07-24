using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public interface IJobActivityLog
    {
        Guid Id { get; }

        string Title { get; }

        IPBSEntity    Product   { get; }
        IDeveloper    Developer { get; }
        DateTime      OnDay     { get; }
        TimeSpan      WorkTime  { get; }
    }
}
