using NUnit.Framework;
using SWP_SchoolMedicalManagementSystem_Service.Repository;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Context;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using Microsoft.EntityFrameworkCore;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Repository;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Repositories
{
    [TestFixture]
    public class StudentRepositoryTests
    {
        private ApplicationDBContext _context;
        private StudentRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "StudentRepoTestDb")
                .Options;
            _context = new ApplicationDBContext(options);
            _repository = new StudentRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetStudentByIdAsync_ReturnsStudent_WhenExists()
        {
            var id = Guid.NewGuid();
            var student = new Student { Id = id, StudentCode = "S001" };
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            var result = await _repository.GetStudentByIdAsync(id);
            Assert.IsNotNull(result);
            Assert.AreEqual("S001", result.StudentCode);
        }

        [Test]
        public async Task GetStudentByIdAsync_ReturnsNull_WhenNotFound()
        {
            var result = await _repository.GetStudentByIdAsync(Guid.NewGuid());
            Assert.IsNull(result);
        }
    }
} 