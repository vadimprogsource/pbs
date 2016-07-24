using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public interface IPBSEntity : IEntity , ITitle
    {
        int Index { get; }
        int Level { get; }
        string Code  { get; }
        IPBSEntity   Root     { get; }
        IPBSEntity   Parent   { get; }
        IPBSEntity[] Children { get; }
        ITechnology Technology { get; } 
        IPerson  OwnedBy  { get; }
    }
}
