using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Map
{
    public abstract class ClassifierMap<TClassifier> : EntityMapper<TClassifier> where TClassifier : Classifier
    {
        public void Map(EntityTypeBuilder<TClassifier> context, string prefix)
        {

            context.Property(x => x.Id).HasColumnName($"{prefix}_id");

            context.Property(x => x.Code).HasColumnName($"{prefix}_code").HasMaxLength(50);
            context.Property(x => x.Name).HasColumnName($"{prefix}_name").HasMaxLength(100);

            context.HasKey(x => x.Id);
        }
    }
}
