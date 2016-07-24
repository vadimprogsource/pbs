using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Map
{
    public class ProjectMap : DocumentMapper<Project>
    {


        public override void Map(EntityTypeBuilder<Project> context)
        {
            Map(context.ForSqlServerToTable("projects"), "project");

            context.Property(x => x.Code       ).HasColumnName("project_code");
            context.Property(x => x.Title      ).HasColumnName("project_title");
            context.Property(x => x.Description).HasColumnName("project_description");

            context.Ignore(x => x.Logs   );
            context.Ignore(x => x.Product);
        }

    }
}
