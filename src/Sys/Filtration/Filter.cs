using Api.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Filtration
{
    public class Filter : IFilter
    {

        public List<FilterCriteria>  m_criterias = new List<FilterCriteria>();


        public  IEnumerable<IFilterCriteria> Criterias
        {
            get
            {
                return m_criterias;
            }
        }

        public int PageIndex { get; set; } = 0;

        public int PageSize { get; set; } = 50;

        public bool HasFiltered
        {
            get
            {
                return m_criterias.Any(x => !string.IsNullOrWhiteSpace(x.FilteredPattern));
            }
        }

        public bool HasOrdered
        {
            get
            {
                return m_criterias.Any(x => x.OrderIsAsc || x.OrderIsDesc);
            }
        }

        public Filter(IFilter filter)
        {
            if (filter != null)
            {
                PageIndex = filter.PageIndex;
                PageSize = filter.PageSize;

                m_criterias = filter.Criterias.Select(x => new FilterCriteria(x)).ToList();

            }
        }

        public Filter()
        {

        }

        public IQueryable<TEntity> Apply<TEntity>(IQueryable<TEntity> query,out bool hasOrdered)
        {

            hasOrdered = false;

            foreach (FilterCriteria critetia in m_criterias)
            {

                if (critetia.OrderIsAsc || critetia.OrderIsDesc)
                {
                    hasOrdered = true;
                }

                query = critetia.ApplyToQuery(query);
            }

            return query;
        }

    }
}
