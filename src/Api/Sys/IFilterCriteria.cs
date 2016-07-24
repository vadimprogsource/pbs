using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Sys
{
    public interface IFilterCriteria
    {
        string FilteredName { get; }
        string FilteredPattern { get; }
        bool OrderIsAsc { get; }
        bool OrderIsDesc { get; }

    }
}
