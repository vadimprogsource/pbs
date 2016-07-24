using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Repository.Map
{
    public class PbsMap : DocumentMapper<PBSEntity>
    {
        public override void Map(EntityTypeBuilder<PBSEntity> context)
        {
            Map(context.ForSqlServerToTable("pbs"), "pbs");


            context.Property(x => x.Id).HasColumnName("pbs_id");
            context.Property(x => x.RootId).HasColumnName("pbs_root_id");
            context.Property(x => x.ParentId).HasColumnName("pbs_parent_id");
            context.Property(x => x.OwnerId).HasColumnName("pbs_owner_id");
            context.Property(x => x.Index).HasColumnName("pbs_index");
            context.Property(x => x.Level).HasColumnName("pbs_level");
            context.Property(x => x.TechId).HasColumnName("pbs_tech_id");
            context.Property(x => x.CreatedBy).HasColumnName("pbs_created_id");
            context.Property(x => x.CreatedOn).HasColumnName("pbs_created_dt");
            context.Property(x => x.ModifiedBy).HasColumnName("pbs_modified_id");
            context.Property(x => x.ModifiedOn).HasColumnName("pbs_modified_dt");
            context.Property(x => x.Code).HasMaxLength(50).HasColumnName("pbs_code");
            context.Property(x => x.Title).HasMaxLength(100).HasColumnName("pbs_title");
            context.Property(x => x.Description).HasColumnName("pbs_description");

            context.Ignore(x => x.Children);
            context.Ignore(x => x.Root);
            context.Ignore(x => x.Parent);
            context.Ignore(x => x.Technology);

            context.HasOne(x => x.Owner ).WithMany().HasForeignKey(x => x.OwnerId);
            context.HasOne(x => x.Technology).WithMany().HasForeignKey(x => x.TechId);

        }
    }
}
