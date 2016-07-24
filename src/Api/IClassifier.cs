using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public interface IClassifier 
    {
        int    Id { get; }
        string Code { get; }
        string Name { get; }

    }
}
