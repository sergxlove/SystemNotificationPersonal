using SystemNotificationPersonal.DataAccess.Sqlite.Models;
using SystemNotificationPersonal.DataAccess.Sqlite.Repositories;

namespace SystemNotificationPeronal.Tests.IntegrationTests
{
    public class UsersRepositoryTests : IntegrationTestBase
    {
        private UsersRepository _repository;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _repository = new UsersRepository(_context);
        }

        [Test]
        public async Task AddAsync_ShouldAddUser_AndReturnId()
        {
            var user = new UsersEntity
            {
                Id = Guid.NewGuid(),
                Login = "testuser",
                Password = "password123",
                Role = "User"
            };
            var id = await _repository.AddAsync(user, CancellationToken.None);
            Assert.That(id, Is.EqualTo(user.Id));
            var savedUser = await _context.UsersTable.FindAsync(id);
            Assert.That(savedUser, Is.Not.Null);
            Assert.That(savedUser.Login, Is.EqualTo("testuser"));
        }

        [Test]
        public async Task UpdatePasswordAsync_ShouldUpdateUserPassword()
        {
            var user = new UsersEntity
            {
                Id = Guid.NewGuid(),
                Login = "user1",
                Password = "oldpassword",
                Role = "User"
            };
            await _repository.AddAsync(user, CancellationToken.None);
            user.Password = "newpassword123";
            var updatedCount = await _repository.UpdatePasswordAsync(user, CancellationToken.None);
            Assert.That(updatedCount, Is.EqualTo(1));
            var updatedUser = await _context.UsersTable.FindAsync(user.Id);
            Assert.That(updatedUser!.Password, Is.EqualTo("newpassword123"));
        }

        [Test]
        public async Task UpdatePasswordAsync_ForNonExistentUser_ShouldReturnZero()
        {
            var nonExistentUser = new UsersEntity
            {
                Id = Guid.NewGuid(),
                Login = "nonexistent",
                Password = "newpass",
                Role = "User"
            };
            var updatedCount = await _repository.UpdatePasswordAsync(nonExistentUser, CancellationToken.None);
            Assert.That(updatedCount, Is.EqualTo(0));
        }

        [Test]
        public async Task UpdateRoleAsync_ShouldUpdateUserRole()
        {
            var user = new UsersEntity
            {
                Id = Guid.NewGuid(),
                Login = "user2",
                Password = "pass",
                Role = "User"
            };
            await _repository.AddAsync(user, CancellationToken.None);
            user.Role = "Administrator";
            var updatedCount = await _repository.UpdateRoleAsync(user, CancellationToken.None);
            Assert.That(updatedCount, Is.EqualTo(1));
            var updatedUser = await _context.UsersTable.FindAsync(user.Id);
            Assert.That(updatedUser!.Role, Is.EqualTo("Administrator"));
        }

        [Test]
        public async Task UpdateRoleAsync_ForNonExistentUser_ShouldReturnZero()
        {
            var nonExistentUser = new UsersEntity
            {
                Id = Guid.NewGuid(),
                Login = "nonexistent",
                Password = "pass",
                Role = "Admin"
            };
            var updatedCount = await _repository.UpdateRoleAsync(nonExistentUser, CancellationToken.None);
            Assert.That(updatedCount, Is.EqualTo(0));
        }

        [Test]
        public async Task DeleteAsync_ShouldRemoveUserByLogin()
        {
            var user = new UsersEntity
            {
                Id = Guid.NewGuid(),
                Login = "todelete",
                Password = "pass",
                Role = "User"
            };
            await _repository.AddAsync(user, CancellationToken.None);
            var deletedCount = await _repository.DeleteAsync("todelete", CancellationToken.None);
            Assert.That(deletedCount, Is.EqualTo(1));
            var exists = await _repository.CheckAsync("todelete", CancellationToken.None);
            Assert.That(exists, Is.False);
        }

        [Test]
        public async Task DeleteAsync_ForNonExistentUser_ShouldReturnZero()
        {
            var deletedCount = await _repository.DeleteAsync("nonexistent", CancellationToken.None);
            Assert.That(deletedCount, Is.EqualTo(0));
        }

        [Test]
        public async Task VerifyAsync_WithValidCredentials_ShouldReturnTrue()
        {
            var user = new UsersEntity
            {
                Id = Guid.NewGuid(),
                Login = "validuser",
                Password = "correctpass",
                Role = "User"
            };
            await _repository.AddAsync(user, CancellationToken.None);
            var isValid = await _repository.VerifyAsync(new UsersEntity
            {
                Login = "validuser",
                Password = "correctpass"
            }, CancellationToken.None);
            Assert.That(isValid, Is.True);
        }

        [Test]
        public async Task VerifyAsync_WithInvalidPassword_ShouldReturnFalse()
        {
            var user = new UsersEntity
            {
                Id = Guid.NewGuid(),
                Login = "validuser",
                Password = "correctpass",
                Role = "User"
            };
            await _repository.AddAsync(user, CancellationToken.None);
            var isValid = await _repository.VerifyAsync(new UsersEntity
            {
                Login = "validuser",
                Password = "wrongpass"
            }, CancellationToken.None);
            Assert.That(isValid, Is.False);
        }

        [Test]
        public async Task VerifyAsync_WithNonExistentLogin_ShouldReturnFalse()
        {
            var isValid = await _repository.VerifyAsync(new UsersEntity
            {
                Login = "nonexistent",
                Password = "anypass"
            }, CancellationToken.None);
            Assert.That(isValid, Is.False);
        }

        [Test]
        public async Task CheckAsync_ForExistingLogin_ShouldReturnTrue()
        {
            var user = new UsersEntity
            {
                Id = Guid.NewGuid(),
                Login = "existinguser",
                Password = "pass",
                Role = "User"
            };
            await _repository.AddAsync(user, CancellationToken.None);
            var exists = await _repository.CheckAsync("existinguser", CancellationToken.None);
            Assert.That(exists, Is.True);
        }

        [Test]
        public async Task CheckAsync_ForNonExistentLogin_ShouldReturnFalse()
        {
            var exists = await _repository.CheckAsync("nonexistent", CancellationToken.None);
            Assert.That(exists, Is.False);
        }

        [Test]
        public async Task MultipleOperations_ShouldWorkCorrectly()
        {
            var user = new UsersEntity
            {
                Id = Guid.NewGuid(),
                Login = "multiuser",
                Password = "pass1",
                Role = "User"
            };
            await _repository.AddAsync(user, CancellationToken.None);
            await _repository.UpdatePasswordAsync(user, CancellationToken.None);
            await _repository.UpdateRoleAsync(user, CancellationToken.None);
            var verified = await _repository.VerifyAsync(user, CancellationToken.None);
            var deleted = await _repository.DeleteAsync(user.Login, CancellationToken.None);
            var checkAfterDelete = await _repository.CheckAsync(user.Login, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(verified, Is.True);
                Assert.That(deleted, Is.EqualTo(1));
                Assert.That(checkAfterDelete, Is.False);
            });
        }

        [Test]
        public async Task AddAsync_MultipleUsers_ShouldSaveAll()
        {
            var users = new List<UsersEntity>();
            for (int i = 0; i < 5; i++)
            {
                users.Add(new UsersEntity
                {
                    Id = Guid.NewGuid(),
                    Login = $"user{i}",
                    Password = $"pass{i}",
                    Role = i % 2 == 0 ? "User" : "Admin"
                });
            }
            foreach (var user in users)
            {
                await _repository.AddAsync(user, CancellationToken.None);
            }
            foreach (var user in users)
            {
                var exists = await _repository.CheckAsync(user.Login, CancellationToken.None);
                Assert.That(exists, Is.True);
            }
        }

        [Test]
        public async Task UpdatePassword_ShouldOnlyAffectSpecifiedUser()
        {
            var user1 = new UsersEntity { Id = Guid.NewGuid(), Login = "user1", Password = "pass1", Role = "User" };
            var user2 = new UsersEntity { Id = Guid.NewGuid(), Login = "user2", Password = "pass2", Role = "User" };
            await _repository.AddAsync(user1, CancellationToken.None);
            await _repository.AddAsync(user2, CancellationToken.None);
            user1.Password = "newpass1";
            await _repository.UpdatePasswordAsync(user1, CancellationToken.None);
            var verifiedUser1 = await _repository.VerifyAsync(new UsersEntity { Login = "user1", Password = "newpass1" }, CancellationToken.None);
            var verifiedUser2 = await _repository.VerifyAsync(new UsersEntity { Login = "user2", Password = "pass2" }, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(verifiedUser1, Is.True);
                Assert.That(verifiedUser2, Is.True);
            });
        }
    }
}
