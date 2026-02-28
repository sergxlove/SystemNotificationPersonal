using Microsoft.EntityFrameworkCore;
using SystemNotificationPersonal.DataAccess.Sqlite.Configurations;
using SystemNotificationPersonal.DataAccess.Sqlite.Models;

namespace SystemNotificationPersonal.DataAccess.Sqlite
{
    public class SystemNotificationDbContext : DbContext
    {
        public SystemNotificationDbContext(DbContextOptions<SystemNotificationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<UsersEntity> UsersTable { get; set; }
        public DbSet<CodesExitEntity> CodesTable { get; set; }
        public DbSet<HistoryNotifyEntity> HistoryNotify {  get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CodesExitConfiguration());
            modelBuilder.ApplyConfiguration(new HistoryNotifyConfiguration());
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
