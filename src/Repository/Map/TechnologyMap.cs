using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Repository.Map
{
    public class TechnologyMap : ClassifierMap<Technology>
    {
        public override void Map(EntityTypeBuilder<Technology> context)
        {
            Map(context.ForSqlServerToTable("tech"), "tech");
        }
    }
}
