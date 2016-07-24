using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api;
using Api.Sys;
using Microsoft.EntityFrameworkCore;
using Sys.Helpers;

namespace Repository
{
    public class DocumentRepository<TInterface, TDocument> : DataRepository<TInterface, TDocument> where TDocument : Document , TInterface,new()
    {
        protected readonly ISecurityContext securityContext;

        public DocumentRepository(IConfig cfg,ISecurityContext securityContext) : base(cfg)
        {
            this.securityContext = securityContext;
        }


        public override IPageResult<TInterface> Select(IFilter selectorFilter)
        {
            return Set<TDocument>().AsNoTracking().Where(selectorFilter).OrderByDefault(x=>x.CreatedOn).SelectPage(x => (TInterface)x);
        }


        protected override TDocument OnCreate(TInterface obj)
        {
            TDocument doc = new TDocument();

            doc.Id        = Guid.NewGuid();
            doc.CreatedOn = doc.ModifiedOn =  DateTime.Now  ;
            doc.CreatedBy = doc.ModifiedBy =  securityContext.Session.Sid;

            return doc;
        }

        protected override void OnUpdate(TInterface outer, TDocument obj)
        {
            obj.ModifiedOn = DateTime.Now;
            obj.ModifiedBy = securityContext.Session.Sid;
        }
    }
}
