using System.Text.RegularExpressions;
using SystemNotificationPersonal.DataAccess.Sqlite.Models;
using SystemNotificationPersonal.DataAccess.Sqlite.Repositories;

namespace SystemNotificationPeronal.Tests.IntegrationTests
{
    public class CodesExitRepositoryTests : IntegrationTestBase
    {
        private CodesExitRepository _repository;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _repository = new CodesExitRepository(_context);
        }

        [Test]
        public async Task GenerateAsync_ShouldCreateNewCodeWithValidFormat()
        {
            var code = await _repository.GenerateAsync(CancellationToken.None);
            Assert.That(code, Is.Not.Null);
            Assert.That(code, Is.Not.Empty);
            Assert.That(code.Length, Is.EqualTo(4));
            Assert.That(code, Does.Match("^[0-9]{4}$"));
        }

        [Test]
        public async Task GenerateAsync_ShouldSaveCodeToDatabase()
        {
            var code = await _repository.GenerateAsync(CancellationToken.None);
            var today = DateOnly.FromDateTime(DateTime.Now);
            var savedCode = await _repository.GetCodeAsync(today, CancellationToken.None);
            Assert.That(savedCode, Is.EqualTo(code));
        }

        [Test]
        public async Task GenerateAsync_ShouldGenerateUniqueCodes()
        {
            var code1 = await _repository.GenerateAsync(CancellationToken.None);
            var code2 = await _repository.GenerateAsync(CancellationToken.None);
            Assert.That(code1, Is.Not.EqualTo(code2));
        }

        [Test]
        public async Task GenerateAsync_MultipleGenerations_ShouldCreateMultipleRecords()
        {
            await _repository.GenerateAsync(CancellationToken.None);
            await _repository.GenerateAsync(CancellationToken.None);
            await _repository.GenerateAsync(CancellationToken.None);
            var allCodes = await _repository.GetCodesAllAsync(CancellationToken.None);
            Assert.That(allCodes.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task GetCodeAsync_WhenCodeExists_ShouldReturnCode()
        {
            var generatedCode = await _repository.GenerateAsync(CancellationToken.None);
            var today = DateOnly.FromDateTime(DateTime.Now);
            var retrievedCode = await _repository.GetCodeAsync(today, CancellationToken.None);
            Assert.That(retrievedCode, Is.EqualTo(generatedCode));
        }

        [Test]
        public async Task GetCodeAsync_WhenCodeDoesNotExist_ShouldReturnEmptyString()
        {
            var futureDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
            var code = await _repository.GetCodeAsync(futureDate, CancellationToken.None);
            Assert.That(code, Is.Empty);
        }

        [Test]
        public async Task GetCodeAsync_WithSpecificDate_ShouldReturnCorrectCode()
        {
            var date1 = DateOnly.FromDateTime(DateTime.Now);
            var date2 = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));
            await _repository.GenerateAsync(CancellationToken.None);
            var oldCode = new CodesExitEntity
            {
                Id = Guid.NewGuid(),
                Date = date2,
                Code = "9999"
            };
            await _context.CodesTable.AddAsync(oldCode);
            await _context.SaveChangesAsync();
            var todayCode = await _repository.GetCodeAsync(date1, CancellationToken.None);
            var yesterdayCode = await _repository.GetCodeAsync(date2, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(todayCode, Is.Not.EqualTo("9999"));
                Assert.That(yesterdayCode, Is.EqualTo("9999"));
            });
        }

        [Test]
        public async Task GetCodesAllAsync_ShouldReturnAllCodes()
        {
            await _repository.GenerateAsync(CancellationToken.None);
            await _repository.GenerateAsync(CancellationToken.None);
            await _repository.GenerateAsync(CancellationToken.None);
            var allCodes = await _repository.GetCodesAllAsync(CancellationToken.None);
            Assert.That(allCodes.Count, Is.EqualTo(3));
            Assert.Multiple(() =>
            {
                Assert.That(allCodes.All(c => c.Code.Length == 4));
                Assert.That(allCodes.All(c => Regex.IsMatch(c.Code, "^[0-9]{4}$")));
            });
        }

        [Test]
        public async Task GetCodesAllAsync_WhenNoCodes_ShouldReturnEmptyList()
        {
            var allCodes = await _repository.GetCodesAllAsync(CancellationToken.None);
            Assert.That(allCodes, Is.Empty);
        }

        [Test]
        public async Task GenerateAsync_ShouldSetCorrectDate()
        {
            await _repository.GenerateAsync(CancellationToken.None);
            var today = DateOnly.FromDateTime(DateTime.Now);
            var code = await _repository.GetCodeAsync(today, CancellationToken.None);
            Assert.That(code, Is.Not.Empty);
            var savedEntity = _context.CodesTable.FirstOrDefault(c => c.Date == today);
            Assert.That(savedEntity, Is.Not.Null);
            Assert.That(savedEntity.Date, Is.EqualTo(today));
        }

        [Test]
        public async Task GenerateAsync_EachCodeShouldHaveUniqueId()
        {
            var ids = new List<Guid>();
            for (int i = 0; i < 5; i++)
            {
                await _repository.GenerateAsync(CancellationToken.None);
            }
            var allCodes = await _repository.GetCodesAllAsync(CancellationToken.None);
            ids = allCodes.Select(c => c.Id).ToList();
            Assert.That(ids.Distinct().Count(), Is.EqualTo(5));
        }

        [Test]
        public async Task GenerateAsync_ShouldBeThreadSafe()
        {
            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(_repository.GenerateAsync(CancellationToken.None));
            }
            await Task.WhenAll(tasks);
            var allCodes = await _repository.GetCodesAllAsync(CancellationToken.None);
            Assert.That(allCodes.Count, Is.EqualTo(10));
        }
    }
}
