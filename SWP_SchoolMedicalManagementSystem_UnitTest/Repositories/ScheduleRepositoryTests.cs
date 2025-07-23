using NUnit.Framework;
using SWP_SchoolMedicalManagementSystem_Service.Repository;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Context;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using SWP_SchoolMedicalManagementSystem_Repository.Repository;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Repositories
{
    [TestFixture]
    public class ScheduleRepositoryTests
    {
        private ApplicationDBContext _context;
        private ScheduleRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "ScheduleRepoTestDb")
                .Options;
            _context = new ApplicationDBContext(options);
            _repository = new ScheduleRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetScheduleByIdAsync_ReturnsSchedule_WhenExists()
        {
            var id = Guid.NewGuid();
            var schedule = new Schedule { Id = id };
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            var result = await _repository.GetScheduleByIdAsync(id);
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        public async Task GetScheduleByIdAsync_ReturnsNull_WhenNotFound()
        {
            var result = await _repository.GetScheduleByIdAsync(Guid.NewGuid());
            Assert.IsNull(result);
        }
    }
} 