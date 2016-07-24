
using Api.Sys;
using System;
using System.Collections.Generic;
using System.Linq;


using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sys.Filtration
{
    public class PageSelector<TEntity>
    {
        protected int                 m_PageIndex;
        protected int                 m_PageSize ;
        protected IQueryable<TEntity> m_query    ;

        private bool ordered_flag = false;

        public int PageIndex { get { return m_PageIndex; } }
        public int PageSize  { get { return m_PageIndex; } }


        public IQueryable<TEntity> AsQueryable { get { return m_query; } }

        internal PageSelector(PageSelector<TEntity> outer)
        {
            m_PageIndex = outer.m_PageIndex;
            m_PageSize  = outer.m_PageSize;
            m_query     = outer.m_query;
        }

        public PageSelector(IQueryable<TEntity> query, IFilter filter)
        {

            Filter dataFilter = new Filter(filter);

            m_PageIndex = dataFilter.PageIndex;
            m_PageSize  = dataFilter.PageSize;

            m_query = new Filter(filter).Apply(query,out ordered_flag);
            
        }


        private PageSelector(int pageIndex, int pageSize, IQueryable<TEntity> query)
        {
            m_PageIndex = pageIndex;
            m_PageSize  = pageSize;
            m_query     = query;
        }



        public PageSelector<TOuterEntity> Select<TOuterEntity>(Expression<Func<TEntity, TOuterEntity>> selector)
        {
            return new PageSelector<TOuterEntity>(m_PageIndex,m_PageSize,m_query.Select(selector));
        }




        public PageSelector<TEntity> OrderByDefault<TOuterEntity>(Expression<Func<TEntity, TOuterEntity>> selector)
        {
            if (!ordered_flag)
            {
                m_query = m_query.OrderBy(selector);
            }

            return this;
        }


        public IPageResult<TInterface> SelectPage<TInterface>(Func<TEntity, TInterface> selector)
        {
            IQueryable<TEntity> query = m_query;

            int totalRecs = query.Count();


            int skipped = m_PageIndex * m_PageSize;

            if (skipped > 0 && skipped < totalRecs)
            {
                query = query.Skip(skipped);
            }

            if (totalRecs > m_PageSize)
            {
                query = query.Take(m_PageSize);
            }


            return new PageResult<TInterface>(totalRecs, m_PageIndex, m_PageSize, query.AsEnumerable().Select(selector));

        }


        public Task<IPageResult<TInterface>> SelectPageAsync<TInterface>(Func<TEntity, TInterface> selector)
        {
            return Task.FromResult(SelectPage(selector));
        }





    }



}
