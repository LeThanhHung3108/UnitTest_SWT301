using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;
using SWP_SchoolMedicalManagementSystem_API.Controllers;
using SWP_SchoolMedicalManagementSystem_Repository.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccFormDto;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Controllers
{
    [TestFixture]
    public class ConsentFormControllerTests
    {
        private Mock<IConsentFormService> _consentFormServiceMock = null!;
        private ConsentFormController _controller = null!;

        [SetUp]
        public void Setup()
        {
            _consentFormServiceMock = new Mock<IConsentFormService>();
            _controller = new ConsentFormController(_consentFormServiceMock.Object);
        }

        [Test]
        public async Task GetAllConsentForms_ReturnsOk_WithList()
        {
            var list = new List<ConsentFormResponse> { new ConsentFormResponse { Id = Guid.NewGuid() } };
            _consentFormServiceMock.Setup(s => s.GetAllConsentFormsAsync()).ReturnsAsync(list);

            var result = await _controller.GetAllConsentForms();
            var okResult = result.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(list, okResult.Value);
        }

        [Test]
        public async Task GetConsentFormById_ReturnsOk_WhenExists()
        {
            var id = Guid.NewGuid();
            var consent = new ConsentFormResponse { Id = id };
            _consentFormServiceMock.Setup(s => s.GetConsentFormByIdAsync(id)).ReturnsAsync(consent);

            var result = await _controller.GetConsentFormById(id);
            var okResult = result.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(consent, okResult.Value);
        }

        [Test]
        public async Task GetConsentFormById_ReturnsNotFound_WhenNotExists()
        {
            var id = Guid.NewGuid();
            _consentFormServiceMock.Setup(s => s.GetConsentFormByIdAsync(id)).ReturnsAsync((ConsentFormResponse)null);

            var result = await _controller.GetConsentFormById(id);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }
    }
} 