using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SystemNotificationPersonal.DataAccess.Sqlite.Models;

namespace SystemNotificationPersonal.DataAccess.Sqlite.Configurations
{
    public class CodesExitConfiguration : IEntityTypeConfiguration<CodesExitEntity>
    {
        public void Configure(EntityTypeBuilder<CodesExitEntity> builder)
        {
            builder.ToTable("codes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Date)
                .IsRequired();
            builder.Property(x => x.Code)
                .IsRequired();
            builder.HasIndex(x => x.Date);
        }
    }
}
