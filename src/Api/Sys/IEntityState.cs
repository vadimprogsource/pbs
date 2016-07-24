using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Sys
{
    public interface IEntityState<TEntity>
    {
        void AssignFrom(TEntity outer);
    }
}
