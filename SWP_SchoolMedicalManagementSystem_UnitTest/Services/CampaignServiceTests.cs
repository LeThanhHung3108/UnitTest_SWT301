using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_Service.Service;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using SWP_SchoolMedicalManagementSystem_Repository.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccCampaignDto;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Services
{
    [TestFixture]
    public class CampaignServiceTests
    {
        private Mock<ICampaignRepository> _campaignRepoMock;
        private Mock<IScheduleRepository> _scheduleRepoMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private Mock<IMapper> _mapperMock;
        private CampaignService _campaignService;

        [SetUp]
        public void Setup()
        {
            _campaignRepoMock = new Mock<ICampaignRepository>();
            _scheduleRepoMock = new Mock<IScheduleRepository>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _mapperMock = new Mock<IMapper>();
            _campaignService = new CampaignService(
                _httpContextAccessorMock.Object,
                _campaignRepoMock.Object,
                _scheduleRepoMock.Object,
                _mapperMock.Object
            );
        }

        [Test]
        public async Task GetCampaignByIdAsync_ReturnsCampaign_WhenExists()
        {
            var id = Guid.NewGuid();
            var campaign = new Campaign { Id = id };
            var campaignDto = new CampaignResponse { Id = id };
            _campaignRepoMock.Setup(r => r.GetCampaignByIdAsync(id)).ReturnsAsync(campaign);
            _mapperMock.Setup(m => m.Map<CampaignResponse>(campaign)).Returns(campaignDto);

            var result = await _campaignService.GetCampaignByIdAsync(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        public void GetCampaignByIdAsync_Throws_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _campaignRepoMock.Setup(r => r.GetCampaignByIdAsync(id)).ReturnsAsync((Campaign)null);

            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _campaignService.GetCampaignByIdAsync(id));
        }
    }
} 