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
    public class MedicalConsultationRepositoryTests
    {
        private ApplicationDBContext _context;
        private MedicalConsultationRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "MedicalConsultationRepoTestDb")
                .Options;
            _context = new ApplicationDBContext(options);
            _repository = new MedicalConsultationRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetMedicalConsultationByIdAsync_ReturnsConsultation_WhenExists()
        {
            var id = Guid.NewGuid();
            var consultation = new MedicalConsultation { Id = id };
            _context.MedicalConsultations.Add(consultation);
            await _context.SaveChangesAsync();

            var found = await _repository.GetMedicalConsultationByIdAsync(id);
            Assert.IsNotNull(found);
            Assert.AreEqual(id, found.Id);
        }

        [Test]
        public async Task GetMedicalConsultationByIdAsync_ReturnsNull_WhenNotFound()
        {
            var found = await _repository.GetMedicalConsultationByIdAsync(Guid.NewGuid());
            Assert.IsNull(found);
        }
    }
} 