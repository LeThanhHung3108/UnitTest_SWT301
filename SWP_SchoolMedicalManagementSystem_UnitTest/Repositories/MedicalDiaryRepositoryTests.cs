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
    public class MedicalDiaryRepositoryTests
    {
        private ApplicationDBContext _context;
        private MedicalDiaryRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "MedicalDiaryRepoTestDb")
                .Options;
            _context = new ApplicationDBContext(options);
            _repository = new MedicalDiaryRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetMedicalDiaryByIdAsync_ReturnsDiary_WhenExists()
        {
            var id = Guid.NewGuid();
            var diary = new MedicalDiary { Id = id };
            _context.MedicalDiaries.Add(diary);
            await _context.SaveChangesAsync();

            var result = await _repository.GetMedicineDiaryById(id);
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        public async Task GetMedicalDiaryByIdAsync_ReturnsNull_WhenNotFound()
        {
            var result = await _repository.GetMedicineDiaryById(Guid.NewGuid());
            Assert.IsNull(result);
        }
    }
} 