using Api.Sys;
using Sys.Filtration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Helpers
{
    public static class PaginationHelper
    {
        public static PageSelector<TEntity> Where<TEntity>(this IQueryable<TEntity> query, IFilter filter)
        {
            return new PageSelector<TEntity>(query, filter);
        }



    }
}
