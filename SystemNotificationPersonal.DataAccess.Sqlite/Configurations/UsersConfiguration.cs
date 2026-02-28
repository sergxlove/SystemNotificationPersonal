using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SystemNotificationPersonal.DataAccess.Sqlite.Models;

namespace SystemNotificationPersonal.DataAccess.Sqlite.Configurations
{
    internal class UsersConfiguration : IEntityTypeConfiguration<UsersEntity>
    {
        public void Configure(EntityTypeBuilder<UsersEntity> builder)
        {
            builder.ToTable("users");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                .IsRequired();
            builder.Property(a => a.Login)
                .IsRequired();
            builder.Property(a => a.Password)
                .IsRequired();
        }
    }
}
