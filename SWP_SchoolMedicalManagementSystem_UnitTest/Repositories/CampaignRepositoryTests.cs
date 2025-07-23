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
    public class CampaignRepositoryTests
    {
        private ApplicationDBContext _context;
        private CampaignRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "CampaignRepoTestDb")
                .Options;
            _context = new ApplicationDBContext(options);
            _repository = new CampaignRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetCampaignByIdAsync_ReturnsCampaign_WhenExists()
        {
            var id = Guid.NewGuid();
            var campaign = new Campaign { Id = id };
            _context.Campaigns.Add(campaign);
            await _context.SaveChangesAsync();

            var found = await _repository.GetCampaignByIdAsync(id);
            Assert.IsNotNull(found);
            Assert.AreEqual(id, found.Id);
        }

        [Test]
        public async Task GetCampaignByIdAsync_ReturnsNull_WhenNotFound()
        {
            var found = await _repository.GetCampaignByIdAsync(Guid.NewGuid());
            Assert.IsNull(found);
        }
    }
} 