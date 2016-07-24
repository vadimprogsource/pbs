using Entity.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Map
{
    public class SessionMap  : EntityMapper<Session>
    {
        public override void Map(EntityTypeBuilder<Session> builder)
        {
            builder.ForSqlServerToTable("sys_sessions");
            builder.Property(x => x.Sid).HasColumnName("session_id");
            builder.Property(x => x.Uid).HasColumnName("session_user_id");
            builder.Property(x => x.Expired).HasColumnName("session_expired_dt");
            builder.Property(x => x.CreatedOn).HasColumnName("session_created_dt");

            builder.HasKey(x => x.Sid);


            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.Uid);

        }
    }
}
