using NUnit.Framework;
using SWP_SchoolMedicalManagementSystem_Service.Repository;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Context;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Implementation;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Repositories
{
    [TestFixture]
    public class HealthRecordRepositoryTests
    {
        private ApplicationDBContext _context;
        private HealthRecordRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "HealthRecordRepoTestDb")
                .Options;
            _context = new ApplicationDBContext(options);
            _repository = new HealthRecordRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetHealthRecordByIdAsync_ReturnsRecord_WhenExists()
        {
            var id = Guid.NewGuid();
            var record = new HealthRecord { Id = id };
            _context.HealthRecords.Add(record);
            await _context.SaveChangesAsync();

            var result = await _repository.GetHealthRecordByIdAsync(id);
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        public async Task GetHealthRecordByIdAsync_ReturnsNull_WhenNotFound()
        {
            var result = await _repository.GetHealthRecordByIdAsync(Guid.NewGuid());
            Assert.IsNull(result);
        }
    }
} 