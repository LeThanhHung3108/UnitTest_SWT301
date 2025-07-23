using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.HealthCheckupResultDto;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;
using SWP_SchoolMedicalManagementSystem_API.Controllers;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Controllers
{
    [TestFixture]
    public class HealthCheckupResultControllerTests
    {
        private Mock<IHealthCheckupResultService> _resultServiceMock = null!;
        private HealthCheckupResultController _controller = null!;

        [SetUp]
        public void Setup()
        {
            _resultServiceMock = new Mock<IHealthCheckupResultService>();
            _controller = new HealthCheckupResultController(_resultServiceMock.Object);
        }

        [Test]
        public async Task GetAllHealthCheckupResults_ReturnsOk_WithList()
        {
            var list = new List<HealthCheckupResponseDto> { new HealthCheckupResponseDto { Id = Guid.NewGuid() } };
            _resultServiceMock.Setup(s => s.GetAll()).ReturnsAsync(list);

            var result = await _controller.GetAll();
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(list, okResult.Value);
        }

        [Test]
        public async Task GetHealthCheckupResultById_ReturnsOk_WhenExists()
        {
            var id = Guid.NewGuid();
            var dto = new HealthCheckupResponseDto { Id = id };
            _resultServiceMock.Setup(s => s.GetById(id)).ReturnsAsync(dto);

            var result = await _controller.GetById(id);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(dto, okResult.Value);
        }

        [Test]
        public async Task GetHealthCheckupResultById_ReturnsNotFound_WhenNotExists()
        {
            var id = Guid.NewGuid();
            _resultServiceMock.Setup(s => s.GetById(id)).ReturnsAsync((HealthCheckupResponseDto)null);

            var result = await _controller.GetById(id);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
} 