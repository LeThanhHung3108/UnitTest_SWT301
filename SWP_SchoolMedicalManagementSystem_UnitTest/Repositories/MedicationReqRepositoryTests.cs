using NUnit.Framework;
using SWP_SchoolMedicalManagementSystem_Service.Repository;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Context;
using Microsoft.EntityFrameworkCore;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Repositories
{
    [TestFixture]
    public class MedicationReqRepositoryTests
    {
        private ApplicationDBContext _context;
        private MedicationReqRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "MedicationReqRepoTestDb")
                .Options;
            _context = new ApplicationDBContext(options);
            _repository = new MedicationReqRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetMedicationRequestById_ReturnsRequest_WhenExists()
        {
            var id = Guid.NewGuid();
            var request = new MedicalRequest { Id = id };
            _context.MedicationRequests.Add(request);
            await _context.SaveChangesAsync();

            var found = await _repository.GetMedicationRequestById(id);
            Assert.IsNotNull(found);
            Assert.AreEqual(id, found.Id);
        }

        [Test]
        public async Task GetMedicationRequestById_ReturnsNull_WhenNotFound()
        {
            var found = await _repository.GetMedicationRequestById(Guid.NewGuid());
            Assert.IsNull(found);
        }
    }
} 