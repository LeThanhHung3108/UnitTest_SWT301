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
    public class VaccResultRepositoryTests
    {
        private ApplicationDBContext _context;
        private VaccResultRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "VaccResultRepoTestDb")
                .Options;
            _context = new ApplicationDBContext(options);
            _repository = new VaccResultRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetVaccResultByIdAsync_ReturnsResult_WhenExists()
        {
            var id = Guid.NewGuid();
            var result = new VaccinationResult { Id = id };
            _context.VaccinationResults.Add(result);
            await _context.SaveChangesAsync();

            var found = await _repository.GetVaccResultByIdAsync(id);
            Assert.IsNotNull(found);
            Assert.AreEqual(id, found.Id);
        }

        [Test]
        public async Task GetVaccResultByIdAsync_ReturnsNull_WhenNotFound()
        {
            var found = await _repository.GetVaccResultByIdAsync(Guid.NewGuid());
            Assert.IsNull(found);
        }
    }
} 