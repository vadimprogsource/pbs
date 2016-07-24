using Api.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Filtration
{
    public class FilterCriteria : IFilterCriteria
    {
        public string FilteredName { get; set; }
        
        public string FilteredPattern { get; set; }
       
        public bool OrderIsAsc { get; set; }
       
        public bool OrderIsDesc { get; set; }


        public FilterCriteria(IFilterCriteria criteria)
        {
            FilteredName    = criteria.FilteredName;
            FilteredPattern = criteria.FilteredPattern;
            OrderIsAsc      = criteria.OrderIsAsc;
            OrderIsDesc     = criteria.OrderIsDesc;
        }


        public FilterCriteria() { }


        internal IQueryable<TEntity> ApplyToQuery<TEntity>(IQueryable<TEntity> query)
        {
            return query;
        }

    }
}
