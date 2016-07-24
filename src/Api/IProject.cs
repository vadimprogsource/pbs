using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public interface IProject
    {
        Guid   Id  { get; }
        string Code { get; }
        string     Title   { get; }
        string Description { get; }
        IPBSEntity Product { get; }
        IJobActivityLog[] Logs { get; } 
    }
}
