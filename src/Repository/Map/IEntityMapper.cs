using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Map
{
   public  interface IEntityMapper<TEntity> where TEntity : class
   {
        void Map(EntityTypeBuilder<TEntity> context);
   }
}
