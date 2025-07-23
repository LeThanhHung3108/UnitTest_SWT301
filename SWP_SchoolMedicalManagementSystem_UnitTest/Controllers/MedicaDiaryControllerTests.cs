using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_API.Controllers;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalDiaryDto;
using Microsoft.AspNetCore.Mvc;


namespace SWP_SchoolMedicalManagementSystem_UnitTest.Controllers
{
    [TestFixture]
    public class MedicaDiaryControllerTests
    {
        private Mock<IMedicalDiaryService> _medicalDiaryServiceMock;
        private MedicaDiaryController _controller;

        [SetUp]
        public void Setup()
        {
            _medicalDiaryServiceMock = new Mock<IMedicalDiaryService>();
            _controller = new MedicaDiaryController(_medicalDiaryServiceMock.Object);
        }

        [Test]
        public async Task GetAllMedicineDiary_ReturnsOk_WithList()
        {
            var list = new List<MedicalDiaryResponseDto> { new MedicalDiaryResponseDto { Id = Guid.NewGuid() } };
            _medicalDiaryServiceMock.Setup(s => s.GetAllMedicineDiary()).ReturnsAsync(list);

            var result = await _controller.GetAllMedicineDiary();
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(list, okResult.Value);
        }

        [Test]
        public async Task GetMedicineDiaryById_ReturnsOk_WhenExists()
        {
            var id = Guid.NewGuid();
            var diary = new MedicalDiaryResponseDto { Id = id };
            _medicalDiaryServiceMock.Setup(s => s.GetMedicineDiaryById(id)).ReturnsAsync(diary);

            var result = await _controller.GetMedicineDiaryById(id);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(diary, okResult.Value);
        }

        [Test]
        public async Task GetMedicineDiaryById_ReturnsNotFound_WhenNotExists()
        {
            var id = Guid.NewGuid();
            _medicalDiaryServiceMock.Setup(s => s.GetMedicineDiaryById(id)).ThrowsAsync(new KeyNotFoundException());

            var result = await _controller.GetMedicineDiaryById(id);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task CreateMedicineDiary_ReturnsOk_WhenValid()
        {
            var req = new MedicalDiaryRequestDto();
            _medicalDiaryServiceMock.Setup(s => s.CreateMedicineDiary(req)).Returns(Task.CompletedTask);

            var result = await _controller.CreateMedicineDiary(req);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task CreateMedicineDiary_ReturnsBadRequest_WhenNull()
        {
            var result = await _controller.CreateMedicineDiary(null);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
} 