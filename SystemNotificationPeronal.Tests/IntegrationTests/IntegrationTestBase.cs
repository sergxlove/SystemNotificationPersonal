using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SystemNotificationPersonal.DataAccess.Sqlite;

namespace SystemNotificationPeronal.Tests.IntegrationTests
{
    public abstract class IntegrationTestBase
    {
        protected SystemNotificationDbContext _context;
        protected SqliteConnection _connection;

        [SetUp]
        public virtual void SetUp()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();
            var options = new DbContextOptionsBuilder<SystemNotificationDbContext>()
                .UseSqlite(_connection)
                .Options;
            _context = new SystemNotificationDbContext(options);
            _context.Database.EnsureCreated();
        }

        [TearDown]
        public virtual void TearDown()
        {
            _context?.Dispose();
            _connection?.Close();
            _connection?.Dispose();
        }
    }
}
