using SystemNotificationPersonal.DataAccess.Sqlite.Models;
using SystemNotificationPersonal.DataAccess.Sqlite.Repositories;

namespace SystemNotificationPeronal.Tests.IntegrationTests
{
    public class HistoryNotifyRepositoryTests : IntegrationTestBase
    {
        private HistoryNotifyRepository _repository;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _repository = new HistoryNotifyRepository(_context);
        }

        private async Task<CodesExitEntity> CreateTestCode()
        {
            var code = new CodesExitEntity
            {
                Id = Guid.NewGuid(),
                Date = DateOnly.FromDateTime(DateTime.Now),
                Code = "1234"
            };
            await _context.CodesTable.AddAsync(code);
            await _context.SaveChangesAsync();
            return code;
        }

        private async Task<UsersEntity> CreateTestUser(string login = "")
        {
            var user = new UsersEntity
            {
                Id = Guid.NewGuid(),
                Login = login ?? $"testuser_{Guid.NewGuid()}",
                Password = "testpass",
                Role = "User"
            };
            await _context.UsersTable.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        [Test]
        public async Task AddAsync_ShouldAddHistoryRecord_AndReturnId()
        {
            var testCode = await CreateTestCode();
            var testUser = await CreateTestUser();
            var history = new HistoryNotifyEntity
            {
                Id = Guid.NewGuid(),
                IdCode = testCode.Id,
                IdUser = testUser.Id,
                TypeAlarm = "Fire",
                DateNotify = DateTime.Now
            };
            var id = await _repository.AddAsync(history, CancellationToken.None);
            Assert.That(id, Is.EqualTo(history.Id));
            var savedHistory = await _context.HistoryNotify.FindAsync(id);
            Assert.That(savedHistory, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(savedHistory.TypeAlarm, Is.EqualTo("Fire"));
                Assert.That(savedHistory.IdCode, Is.EqualTo(testCode.Id));
                Assert.That(savedHistory.IdUser, Is.EqualTo(testUser.Id));
            });
        }

        [Test]
        public async Task AddAsync_MultipleRecords_ShouldSaveAll()
        {
            var testCode = await CreateTestCode();
            var testUser = await CreateTestUser();
            var histories = new List<HistoryNotifyEntity>
            {
                new() { Id = Guid.NewGuid(), IdCode = testCode.Id, IdUser = testUser.Id,
                        DateNotify = DateTime.Now, TypeAlarm = "Fire" },
                new() { Id = Guid.NewGuid(), IdCode = testCode.Id, IdUser = testUser.Id,
                        DateNotify = DateTime.Now, TypeAlarm = "Alarm" },
                new() { Id = Guid.NewGuid(), IdCode = testCode.Id, IdUser = testUser.Id,
                        DateNotify = DateTime.Now, TypeAlarm = "Intrusion" }
            };
            foreach (var history in histories)
            {
                await _repository.AddAsync(history, CancellationToken.None);
            }
            var allHistories = await _repository.GetAsync(CancellationToken.None);
            Assert.That(allHistories.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task AddAsync_WithDifferentUsersAndCodes_ShouldSaveCorrectRelations()
        {
            var testCode1 = await CreateTestCode();
            var testCode2 = await CreateTestCode();
            var testUser1 = await CreateTestUser("user1");
            var testUser2 = await CreateTestUser("user2");
            var history1 = new HistoryNotifyEntity
            {
                Id = Guid.NewGuid(),
                IdCode = testCode1.Id,
                IdUser = testUser1.Id,
                TypeAlarm = "Fire",
                DateNotify = DateTime.Now
            };
            var history2 = new HistoryNotifyEntity
            {
                Id = Guid.NewGuid(),
                IdCode = testCode2.Id,
                IdUser = testUser2.Id,
                TypeAlarm = "Alarm",
                DateNotify = DateTime.Now
            };
            await _repository.AddAsync(history1, CancellationToken.None);
            await _repository.AddAsync(history2, CancellationToken.None);
            var savedHistory1 = await _context.HistoryNotify.FindAsync(history1.Id);
            var savedHistory2 = await _context.HistoryNotify.FindAsync(history2.Id);
            Assert.Multiple(() =>
            {
                Assert.That(savedHistory1!.IdCode, Is.EqualTo(testCode1.Id));
                Assert.That(savedHistory1.IdUser, Is.EqualTo(testUser1.Id));
                Assert.That(savedHistory2!.IdCode, Is.EqualTo(testCode2.Id));
                Assert.That(savedHistory2.IdUser, Is.EqualTo(testUser2.Id));
            });
        }

        [Test]
        public async Task GetByDateAsync_ShouldReturnRecordsForSpecificDate()
        {
            var testCode = await CreateTestCode();
            var testUser = await CreateTestUser();
            var targetDate = new DateTime(2024, 1, 15);
            await _repository.AddAsync(new HistoryNotifyEntity
            {
                Id = Guid.NewGuid(),
                IdCode = testCode.Id,
                IdUser = testUser.Id,
                DateNotify = targetDate,
                TypeAlarm = "Fire"
            }, CancellationToken.None);
            await _repository.AddAsync(new HistoryNotifyEntity
            {
                Id = Guid.NewGuid(),
                IdCode = testCode.Id,
                IdUser = testUser.Id,
                DateNotify = targetDate.AddDays(1),
                TypeAlarm = "Alarm"
            }, CancellationToken.None);
            var result = await _repository.GetByDateAsync(targetDate, CancellationToken.None);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].TypeAlarm, Is.EqualTo("Fire"));
                Assert.That(result[0].DateNotify.Date, Is.EqualTo(targetDate.Date));
            });
        }

        [Test]
        public async Task GetByDateAsync_WithMultipleRecordsSameDate_ShouldReturnAll()
        {
            var testCode = await CreateTestCode();
            var testUser = await CreateTestUser();
            var targetDate = DateTime.Now.Date;
            for (int i = 0; i < 5; i++)
            {
                await _repository.AddAsync(new HistoryNotifyEntity
                {
                    Id = Guid.NewGuid(),
                    IdCode = testCode.Id,
                    IdUser = testUser.Id,
                    DateNotify = targetDate,
                    TypeAlarm = $"Alarm_{i}"
                }, CancellationToken.None);
            }
            var result = await _repository.GetByDateAsync(targetDate, CancellationToken.None);
            Assert.That(result.Count, Is.EqualTo(5));
            Assert.That(result.All(r => r.DateNotify.Date == targetDate));
        }

        [Test]
        public async Task GetByDateAsync_WithNoRecords_ShouldReturnEmptyList()
        {
            var futureDate = DateTime.Now.AddYears(1);
            var result = await _repository.GetByDateAsync(futureDate, CancellationToken.None);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task GetByTypeAlarmAsync_ShouldReturnRecordsWithSpecificType()
        {
            var testCode = await CreateTestCode();
            var testUser = await CreateTestUser();
            await _repository.AddAsync(new HistoryNotifyEntity
            {
                Id = Guid.NewGuid(),
                IdCode = testCode.Id,
                IdUser = testUser.Id,
                DateNotify = DateTime.Now,
                TypeAlarm = "Fire"
            }, CancellationToken.None);
            await _repository.AddAsync(new HistoryNotifyEntity
            {
                Id = Guid.NewGuid(),
                IdCode = testCode.Id,
                IdUser = testUser.Id,
                DateNotify = DateTime.Now,
                TypeAlarm = "Fire"
            }, CancellationToken.None);
            await _repository.AddAsync(new HistoryNotifyEntity
            {
                Id = Guid.NewGuid(),
                IdCode = testCode.Id,
                IdUser = testUser.Id,
                DateNotify = DateTime.Now,
                TypeAlarm = "Intrusion"
            }, CancellationToken.None);
            var fireAlarms = await _repository.GetByTypeAlarmAsync("Fire", CancellationToken.None);
            Assert.That(fireAlarms.Count, Is.EqualTo(2));
            Assert.That(fireAlarms.All(a => a.TypeAlarm == "Fire"));
        }

        [Test]
        public async Task GetByTypeAlarmAsync_WithNonExistentType_ShouldReturnEmptyList()
        {
            var result = await _repository.GetByTypeAlarmAsync("NonExistent", CancellationToken.None);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task GetByTypeAlarmAsync_ShouldPreserveRelatedIds()
        {
            var testCode = await CreateTestCode();
            var testUser = await CreateTestUser();
            var expectedHistory = new HistoryNotifyEntity
            {
                Id = Guid.NewGuid(),
                IdCode = testCode.Id,
                IdUser = testUser.Id,
                DateNotify = DateTime.Now,
                TypeAlarm = "Critical"
            };
            await _repository.AddAsync(expectedHistory, CancellationToken.None);
            var result = await _repository.GetByTypeAlarmAsync("Critical", CancellationToken.None);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].IdCode, Is.EqualTo(testCode.Id));
                Assert.That(result[0].IdUser, Is.EqualTo(testUser.Id));
            });
        }

        [Test]
        public async Task GetAsync_ShouldReturnAllHistoryRecords()
        {
            var testCode = await CreateTestCode();
            var testUser = await CreateTestUser();
            for (int i = 0; i < 10; i++)
            {
                await _repository.AddAsync(new HistoryNotifyEntity
                {
                    Id = Guid.NewGuid(),
                    IdCode = testCode.Id,
                    IdUser = testUser.Id,
                    DateNotify = DateTime.Now.AddDays(-i),
                    TypeAlarm = i % 2 == 0 ? "Fire" : "Alarm"
                }, CancellationToken.None);
            }
            var allRecords = await _repository.GetAsync(CancellationToken.None);
            Assert.That(allRecords.Count, Is.EqualTo(10));
        }

        [Test]
        public async Task GetAsync_WhenNoRecords_ShouldReturnEmptyList()
        {
            var allRecords = await _repository.GetAsync(CancellationToken.None);
            Assert.That(allRecords, Is.Empty);
        }

        [Test]
        public async Task AddAsync_ShouldPreserveAllProperties()
        {
            var testCode = await CreateTestCode();
            var testUser = await CreateTestUser();
            var expectedId = Guid.NewGuid();
            var expectedDate = new DateTime(2024, 12, 25, 14, 30, 0);
            var expectedHistory = new HistoryNotifyEntity
            {
                Id = expectedId,
                IdCode = testCode.Id,
                IdUser = testUser.Id,
                DateNotify = expectedDate,
                TypeAlarm = "Critical"
            };
            await _repository.AddAsync(expectedHistory, CancellationToken.None);
            var savedHistory = await _context.HistoryNotify.FindAsync(expectedId);
            Assert.That(savedHistory, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(savedHistory.Id, Is.EqualTo(expectedId));
                Assert.That(savedHistory.IdCode, Is.EqualTo(testCode.Id));
                Assert.That(savedHistory.IdUser, Is.EqualTo(testUser.Id));
                Assert.That(savedHistory.DateNotify, Is.EqualTo(expectedDate));
                Assert.That(savedHistory.TypeAlarm, Is.EqualTo("Critical"));
            });
        }

        [Test]
        public async Task AddAsync_WithEmptyTypeAlarm_ShouldSaveEmptyString()
        {
            var testCode = await CreateTestCode();
            var testUser = await CreateTestUser();
            var history = new HistoryNotifyEntity
            {
                Id = Guid.NewGuid(),
                IdCode = testCode.Id,
                IdUser = testUser.Id,
                TypeAlarm = string.Empty,
                DateNotify = DateTime.Now
            };
            var id = await _repository.AddAsync(history, CancellationToken.None);
            var savedHistory = await _context.HistoryNotify.FindAsync(id);
            Assert.That(savedHistory!.TypeAlarm, Is.EqualTo(string.Empty));
        }

        [Test]
        public async Task GetByDateAsync_ShouldReturnRecordsWithCorrectIds()
        {
            var testCode1 = await CreateTestCode();
            var testCode2 = await CreateTestCode();
            var testUser = await CreateTestUser();
            var targetDate = DateTime.Now.Date;
            var historyWithCode1 = new HistoryNotifyEntity
            {
                Id = Guid.NewGuid(),
                IdCode = testCode1.Id,
                IdUser = testUser.Id,
                DateNotify = targetDate,
                TypeAlarm = "Alarm1"
            };
            var historyWithCode2 = new HistoryNotifyEntity
            {
                Id = Guid.NewGuid(),
                IdCode = testCode2.Id,
                IdUser = testUser.Id,
                DateNotify = targetDate,
                TypeAlarm = "Alarm2"
            };
            await _repository.AddAsync(historyWithCode1, CancellationToken.None);
            await _repository.AddAsync(historyWithCode2, CancellationToken.None);
            var result = await _repository.GetByDateAsync(targetDate, CancellationToken.None);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(result.Any(r => r.IdCode == testCode1.Id));
                Assert.That(result.Any(r => r.IdCode == testCode2.Id));
            });
        }

        [Test]
        public async Task MultipleOperations_ShouldMaintainDataIntegrity()
        {
            var testCode = await CreateTestCode();
            var testUser = await CreateTestUser();
            var history1 = new HistoryNotifyEntity
            {
                Id = Guid.NewGuid(),
                IdCode = testCode.Id,
                IdUser = testUser.Id,
                DateNotify = DateTime.Now,
                TypeAlarm = "Type1"
            };
            await _repository.AddAsync(history1, CancellationToken.None);
            var history2 = new HistoryNotifyEntity
            {
                Id = Guid.NewGuid(),
                IdCode = testCode.Id,
                IdUser = testUser.Id,
                DateNotify = DateTime.Now.AddHours(1),
                TypeAlarm = "Type2"
            };

            await _repository.AddAsync(history2, CancellationToken.None);

            var allRecords = await _repository.GetAsync(CancellationToken.None);
            var type1Records = await _repository.GetByTypeAlarmAsync("Type1", CancellationToken.None);
            var dateRecords = await _repository.GetByDateAsync(DateTime.Now.Date, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(allRecords.Count, Is.EqualTo(2));
                Assert.That(type1Records.Count, Is.EqualTo(1));
                Assert.That(dateRecords.Count, Is.EqualTo(2));
            });
        }
    }
}
