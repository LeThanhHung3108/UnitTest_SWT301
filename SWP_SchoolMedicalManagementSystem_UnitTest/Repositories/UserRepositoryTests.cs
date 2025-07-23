using NUnit.Framework;
using SWP_SchoolMedicalManagementSystem_Service.Repository;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Context;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using Microsoft.EntityFrameworkCore;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Repositories
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private ApplicationDBContext _context;
        private UserRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "UserRepoTestDb")
                .Options;
            _context = new ApplicationDBContext(options);
            _repository = new UserRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddUserAsync_AddsUser()
        {
            var user = new User { Id = Guid.NewGuid(), Username = "test" };
            await _repository.AddUserAsync(user);
            var result = await _repository.GetUserByIdAsync(user.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual("test", result.Username);
        }

        [Test]
        public async Task GetUserByIdAsync_ReturnsNull_WhenNotFound()
        {
            var result = await _repository.GetUserByIdAsync(Guid.NewGuid());
            Assert.IsNull(result);
        }
    }
} 