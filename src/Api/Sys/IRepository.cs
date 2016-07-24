using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Sys
{
    public interface IRepository<TEntity>
    {
        IPageResult<TEntity> Select(IFilter selectorFilter);
        TEntity Select(Guid id);
        TEntity Insert(TEntity entityState);
        TEntity Update(TEntity entityState);
        bool    Delete(Guid id);
    }
}
