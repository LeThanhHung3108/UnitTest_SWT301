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
    public class ConsentFormRepositoryTests
    {
        private ApplicationDBContext _context;
        private ConsentFormRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "ConsentFormRepoTestDb")
                .Options;
            _context = new ApplicationDBContext(options);
            _repository = new ConsentFormRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetConsentFormByIdAsync_ReturnsConsentForm_WhenExists()
        {
            var id = Guid.NewGuid();
            var consentForm = new ConsentForm { Id = id };
            _context.ConsentForms.Add(consentForm);
            await _context.SaveChangesAsync();

            var found = await _repository.GetConsentFormByIdAsync(id);
            Assert.IsNotNull(found);
            Assert.AreEqual(id, found.Id);
        }

        [Test]
        public async Task GetConsentFormByIdAsync_ReturnsNull_WhenNotFound()
        {
            var found = await _repository.GetConsentFormByIdAsync(Guid.NewGuid());
            Assert.IsNull(found);
        }
    }
} 