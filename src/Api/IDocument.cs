using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public interface IDocument
    {
        DateTime CreatedOn { get; }
        Guid     CreatedBy { get; }

        DateTime ModifiedOn { get; }
        Guid     ModifiedBy { get; }
    }
}
