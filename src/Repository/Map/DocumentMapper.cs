using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Api;

namespace Repository.Map
{
    public abstract class DocumentMapper<TDocument> : EntityMapper<TDocument> where TDocument : Document
    {
        public  void Map(EntityTypeBuilder<TDocument> context, string prefix)
        {

            context.Property(x => x.Id).HasColumnName($"{prefix}_id");

            context.Property(x => x.CreatedOn).HasColumnName($"{prefix}_created_dt");
            context.Property(x => x.CreatedBy).HasColumnName($"{prefix}_created_id"); 

            context.Property(x => x.ModifiedBy).HasColumnName($"{prefix}_modified_id"); 
            context.Property(x => x.ModifiedOn).HasColumnName($"{prefix}_modified_dt");

            context.HasKey(x => x.Id); 
        }
    }
}
