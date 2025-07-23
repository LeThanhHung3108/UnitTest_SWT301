using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;
using SWP_SchoolMedicalManagementSystem_API.Controllers;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccCampaignDto;
using SchoolMedicalManagementSystem.Enum;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Controllers
{
    [TestFixture]
    public class CampaignControllerTests
    {
        private Mock<ICampaignService> _campaignServiceMock = null!;
        private CampaignController _controller = null!;

        [SetUp]
        public void Setup()
        {
            _campaignServiceMock = new Mock<ICampaignService>();
            _controller = new CampaignController(_campaignServiceMock.Object);
        }

        [Test]
        public async Task GetCampaignById_ReturnsOk_WhenExists()
        {
            var id = Guid.NewGuid();
            var campaign = new CampaignResponse { Id = id };
            _campaignServiceMock.Setup(s => s.GetCampaignByIdAsync(id)).ReturnsAsync(campaign);

            var result = await _controller.GetVaccCampaignById(id);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(campaign, okResult.Value);
        }

        [Test]
        public async Task GetCampaignById_ReturnsNotFound_WhenNotExists()
        {
            var id = Guid.NewGuid();
            _campaignServiceMock.Setup(s => s.GetCampaignByIdAsync(id)).ThrowsAsync(new KeyNotFoundException());

            var result = await _controller.GetVaccCampaignById(id);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task GetAllCampaigns_ReturnsOk_WithList()
        {
            var campaigns = new List<CampaignResponse> { new CampaignResponse { Id = Guid.NewGuid() } };
            _campaignServiceMock.Setup(s => s.GetAllCampaignsAsync()).ReturnsAsync(campaigns);

            var result = await _controller.GetAllCampaigns();
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(campaigns, okResult.Value);
        }

        [Test]
        public async Task GetCampaignsByStatus_ReturnsOk_WhenExists()
        {
            var campaigns = new List<CampaignResponse> { new CampaignResponse { Id = Guid.NewGuid() } };
            _campaignServiceMock.Setup(s => s.GetCampaignsByStatusAsync(It.IsAny<CampaignStatus>())).ReturnsAsync(campaigns);

            var result = await _controller.GetCampaignsByStatus(CampaignStatus.Planned);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(campaigns, okResult.Value);
        }

        [Test]
        public async Task GetCampaignsByStatus_ReturnsNotFound_WhenNone()
        {
            _campaignServiceMock.Setup(s => s.GetCampaignsByStatusAsync(It.IsAny<CampaignStatus>())).ReturnsAsync(new List<CampaignResponse>());

            var result = await _controller.GetCampaignsByStatus(CampaignStatus.Planned);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task GetCampaignsByType_ReturnsOk_WhenExists()
        {
            var campaigns = new List<CampaignResponse> { new CampaignResponse { Id = Guid.NewGuid() } };
            _campaignServiceMock.Setup(s => s.GetCampaignsByTypeAsync(It.IsAny<CampaignType>())).ReturnsAsync(campaigns);

            var result = await _controller.GetCampaignsByType(CampaignType.Vaccination);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(campaigns, okResult.Value);
        }

        [Test]
        public async Task GetCampaignsByType_ReturnsNotFound_WhenNone()
        {
            _campaignServiceMock.Setup(s => s.GetCampaignsByTypeAsync(It.IsAny<CampaignType>())).ReturnsAsync(new List<CampaignResponse>());

            var result = await _controller.GetCampaignsByType(CampaignType.Vaccination);
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task CreateCampaign_ReturnsOk_WhenValid()
        {
            var request = new CampaignRequest();
            _campaignServiceMock.Setup(s => s.CreateCampaignAsync(request)).Returns(Task.CompletedTask);

            var result = await _controller.CreateCampaign(request);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task CreateCampaign_ReturnsBadRequest_WhenNull()
        {
            var result = await _controller.CreateCampaign(null);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task UpdateCampaign_ReturnsOk_WhenValid()
        {
            var request = new CampaignRequest();
            _campaignServiceMock.Setup(s => s.UpdateCampaignAsync(It.IsAny<Guid>(), request)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateCampaign(Guid.NewGuid(), request);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task UpdateCampaign_ReturnsBadRequest_WhenNull()
        {
            var result = await _controller.UpdateCampaign(Guid.NewGuid(), null);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task DeleteCampaign_ReturnsOk()
        {
            _campaignServiceMock.Setup(s => s.DeleteCampaignAsync(It.IsAny<Guid>())).Returns(Task.CompletedTask);

            var result = await _controller.DeleteCampaign(Guid.NewGuid());
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
} 