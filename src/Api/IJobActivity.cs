using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public  interface IJobActivity
    {
        IPBSEntity Product { get; }

        TimeSpan Estimation { get; }
        TimeSpan Effort     { get; }


        DateTime StartDateTime { get; }
        DateTime EndDateTime   { get; }
        DateTime Deadline      { get; }
    }
}
