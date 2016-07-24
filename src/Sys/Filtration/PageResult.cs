using Api.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Filtration
{


    public class PageResult<TEntity> : IPageResult<TEntity>
    {

        private TEntity[] m_page;


   
        public PageResult(IEnumerable<TEntity> enumerable)
        {
            m_page    = enumerable.ToArray();
            TotalRecs = PageSize = m_page.Length;
            PageIndex = 0;
        }


        public PageResult(int totalRecs, int pageIndex , int pageSize,IEnumerable<TEntity> page)
        {
            TotalRecs = totalRecs;
            PageSize  = pageSize;
            PageIndex = pageIndex;

            m_page = page.ToArray();
        }


        public IEnumerable<TEntity> Page
        {
            get
            {
                return m_page;
            }
        }

        public int PageIndex { get; }
    
        public int PageSize { get; }
       
        public int TotalPages
        {
            get
           {
                int i = TotalRecs / PageSize;

                if (TotalRecs % PageSize > 0)
                {
                    ++i;
                }

                return i;
           }
        }
       
        public int TotalRecs
        {
            get;
        }


        public PageResult<TOuterEntity> Select<TOuterEntity>(Func<TEntity, TOuterEntity> selector)
        {
            return new PageResult<TOuterEntity>(TotalRecs, PageIndex, PageSize, m_page.Select(selector));
        }

        public IPageResult<TOuterEntity> OfType<TOuterEntity>()
        {
            return new PageResult<TOuterEntity>(TotalRecs, PageIndex, PageSize, m_page.OfType<TOuterEntity>());
        }

        public Task<IPageResult<TOuterEntity>> OfTypeAsync<TOuterEntity>()
        {
            return Task.FromResult(OfType<TOuterEntity>());
        }
    }
}
