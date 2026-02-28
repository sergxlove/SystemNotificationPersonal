using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SystemNotificationPersonal.DataAccess.Sqlite.Models;

namespace SystemNotificationPersonal.DataAccess.Sqlite.Configurations
{
    public class HistoryNotifyConfiguration : IEntityTypeConfiguration<HistoryNotifyEntity>
    {
        public void Configure(EntityTypeBuilder<HistoryNotifyEntity> builder)
        {
            builder.ToTable("history");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.IdCode)
                .IsRequired();
            builder.Property(a => a.IdUser)
                .IsRequired();
            builder.Property(a => a.TypeAlarm)
                .IsRequired();
            builder.Property(a => a.DateNotify)
                .IsRequired();
        }
    }
}
