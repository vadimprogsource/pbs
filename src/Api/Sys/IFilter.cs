using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Sys
{
    public interface IFilter
    {
        int PageIndex { get; }
        int PageSize { get; }

        IEnumerable<IFilterCriteria> Criterias { get; }

        bool HasFiltered { get; }
        bool HasOrdered  { get; }
    }
}
