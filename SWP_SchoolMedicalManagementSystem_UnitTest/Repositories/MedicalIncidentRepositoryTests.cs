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
    public class MedicalIncidentRepositoryTests
    {
        private ApplicationDBContext _context;
        private MedicalIncidentRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "MedicalIncidentRepoTestDb")
                .Options;
            _context = new ApplicationDBContext(options);
            _repository = new MedicalIncidentRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetIncidentByIdAsync_ReturnsIncident_WhenExists()
        {
            var id = Guid.NewGuid();
            var incident = new MedicalIncident { Id = id };
            _context.MedicalIncidents.Add(incident);
            await _context.SaveChangesAsync();

            var found = await _repository.GetIncidentByIdAsync(id);
            Assert.IsNotNull(found);
            Assert.AreEqual(id, found.Id);
        }

        [Test]
        public async Task GetIncidentByIdAsync_ReturnsNull_WhenNotFound()
        {
            var found = await _repository.GetIncidentByIdAsync(Guid.NewGuid());
            Assert.IsNull(found);
        }
    }
} 