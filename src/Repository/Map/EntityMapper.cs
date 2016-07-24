using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Map
{
    public abstract class EntityMapper<TEntity> : IModelMapper , IEntityMapper<TEntity> where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> context);
      
        public void Map(ModelBuilder context)
        {
            Map(context.Entity<TEntity>());
        }
    }
}
