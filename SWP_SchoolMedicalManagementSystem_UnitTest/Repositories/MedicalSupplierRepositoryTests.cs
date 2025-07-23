using NUnit.Framework;
using SWP_SchoolMedicalManagementSystem_Service.Repository;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Context;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Moq;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Repositories
{
    [TestFixture]
    public class MedicalSupplierRepositoryTests
    {
        private ApplicationDBContext _context;
        private MedicalSuplierRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "MedicalSupplierRepoTestDb")
                .Options;
            _context = new ApplicationDBContext(options);
            var mapper = new Mock<AutoMapper.IMapper>().Object;
            _repository = new MedicalSuplierRepository(_context, mapper);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetSupplierByIdAsync_ReturnsSupplier_WhenExists()
        {
            var id = Guid.NewGuid();
            var supplier = new MedicalSupply { Id = id };
            _context.MedicalSupplies.Add(supplier);
            await _context.SaveChangesAsync();

            var found = await _repository.GetSupplierByIdAsync(id);
            Assert.IsNotNull(found);
            Assert.AreEqual(id, found.Id);
        }

        [Test]
        public async Task GetSupplierByIdAsync_ReturnsNull_WhenNotFound()
        {
            var found = await _repository.GetSupplierByIdAsync(Guid.NewGuid());
            Assert.IsNull(found);
        }
    }
} 