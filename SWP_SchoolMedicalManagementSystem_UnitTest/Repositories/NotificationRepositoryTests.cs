using NUnit.Framework;
using SWP_SchoolMedicalManagementSystem_Service.Repository;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Context;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Repositories
{
    [TestFixture]
    public class NotificationRepositoryTests
    {
        private ApplicationDBContext _context;
        private NotificationRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "NotificationRepoTestDb")
                .Options;
            _context = new ApplicationDBContext(options);
            _repository = new NotificationRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetNotificationByIdAsync_ReturnsNotification_WhenExists()
        {
            var id = Guid.NewGuid();
            var notification = new Notification { Id = id };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            var result = await _repository.GetNotificationByIdAsync(id);
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        public async Task GetNotificationByIdAsync_ReturnsNull_WhenNotFound()
        {
            var result = await _repository.GetNotificationByIdAsync(Guid.NewGuid());
            Assert.IsNull(result);
        }
    }
} 