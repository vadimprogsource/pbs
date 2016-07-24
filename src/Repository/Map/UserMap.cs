using Entity.Security;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository.Map
{
    public class UserMap : EntityMapper<User>
    {
        public override void Map(EntityTypeBuilder<User> builder)
        {
            builder.ForSqlServerToTable("sys_users");
            builder.Property(x => x.Id).HasColumnName("user_id");
            builder.Property(x => x.Title).HasMaxLength(100).HasColumnName("user_title");
            builder.Property(x => x.LoginName).HasMaxLength(50).HasColumnName("user_login_name");
            builder.Property(x => x.IsActive).HasColumnName("user_active_flag");
            builder.Property(x => x.IsAdmin).HasColumnName("user_admin_flag");
            builder.Property(x => x.Password).HasColumnName("user_password");
            builder.Property(x => x.ModifiedOn).HasColumnName("user_modified_dt");

            builder.Property(x => x.Skype      ).HasMaxLength(50).HasColumnName("user_skype");
            builder.Property(x => x.Phone      ).HasMaxLength(50).HasColumnName("user_phone");
            builder.Property(x => x.Email      ).HasMaxLength(50).HasColumnName("user_email");
            builder.Property(x => x.Description).HasMaxLength(50).HasColumnName("user_description");

            builder.HasKey(x => x.Id);
        }

    }
}
