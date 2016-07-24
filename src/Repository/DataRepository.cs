using Api;
using Api.Repository;
using Api.Sys;
using Microsoft.EntityFrameworkCore;
using Repository.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Sys.Helpers;

namespace Repository
{
    public class DataRepository : DbContext
    {

        private static IModelMapper[] mappers;

        static DataRepository()
        {

           mappers =  typeof(DataRepository)
                       .GetTypeInfo()
                       .Assembly
                       .GetTypes()
                       .Where(x =>x.GetTypeInfo().IsClass &&  typeof(IModelMapper).IsAssignableFrom(x) && !x.GetTypeInfo().IsAbstract)
                       .Select(x => Activator.CreateInstance(x))
                       .OfType<IModelMapper>()
                       .ToArray();
        }

        public DataRepository(IConfig cfg) : base(new DbContextOptionsBuilder().UseSqlServer(cfg.ConnectionString).Options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (IModelMapper x in mappers)
            {
                x.Map(modelBuilder);
            }
        }
    }



    public abstract class DataRepository<TInterface, TEntity> : DataRepository , IRepository<TInterface> where TEntity : class,IEntity, TInterface ,new()
    {
        public DataRepository(IConfig cfg) : base(cfg) { }


        public virtual TInterface Select(Guid id)
        {
            return Set<TEntity>().AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public virtual IPageResult<TInterface> Select(IFilter selectorFilter)
        {
            return Set<TEntity>().AsNoTracking().Where(selectorFilter).SelectPage(x=>(TInterface)x);
        }


        protected abstract TEntity OnCreate(TInterface obj);
      
        public virtual TInterface Insert(TInterface entityState)
        {
            TEntity x = OnCreate(entityState);

            Set<TEntity>().Add(x);
            SaveChanges ();

            return x;
        }


        protected abstract void OnUpdate(TInterface outer, TEntity obj);


        public virtual TInterface Update(TInterface entityState)
        {

            Guid id = (entityState as IEntity).Id;


            TEntity dbEntity = Set<TEntity>().FirstOrDefault(x => x.Id == id);


            if (dbEntity != null)
            {
                OnUpdate(entityState, dbEntity);
                SaveChanges();
            }

            return dbEntity;
        }


        public virtual bool Delete(Guid id)
        {
            DbSet<TEntity> entitySet = Set<TEntity>();

            TEntity entityState = entitySet.FirstOrDefault(x => x.Id == id);

            if(entityState!=null)
            {

                entitySet.Remove(entityState);
                return SaveChanges() > 0;
            }

            return false;
        }
       
    }
}
