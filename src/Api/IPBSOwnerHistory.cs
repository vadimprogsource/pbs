using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public interface IPBSOwnerHistory
    {
         DateTime CreatedOn { get; }
         IPBSEntity PbsElement { get; }
         IPerson    PbsOwner { get; }
        string Description { get; }
         
    }
}
