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
    public class HealthCheckupResultRepositoryTests
    {
        private ApplicationDBContext _context;
        private HealthCheckupResultRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "HealthCheckupResultRepoTestDb")
                .Options;
            _context = new ApplicationDBContext(options);
            _repository = new HealthCheckupResultRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetById_ReturnsResult_WhenExists()
        {
            var id = Guid.NewGuid();
            var result = new HealthCheckupResult { Id = id };
            _context.HealthCheckupResults.Add(result);
            await _context.SaveChangesAsync();

            var found = await _repository.GetById(id);
            Assert.IsNotNull(found);
            Assert.AreEqual(id, found.Id);
        }

        [Test]
        public async Task GetById_ReturnsNull_WhenNotFound()
        {
            var found = await _repository.GetById(Guid.NewGuid());
            Assert.IsNull(found);
        }
    }
} 