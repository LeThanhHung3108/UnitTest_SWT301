using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_API.Controllers;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccResultDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Controllers
{
    [TestFixture]
    public class VaccResultControllerTests
    {
        private Mock<IVaccResultService> _vaccResultServiceMock;
        private VaccResultController _controller;

        [SetUp]
        public void Setup()
        {
            _vaccResultServiceMock = new Mock<IVaccResultService>();
            _controller = new VaccResultController(_vaccResultServiceMock.Object);
        }

        [Test]
        public async Task GetVaccResultById_ReturnsOk_WhenExists()
        {
            var id = Guid.NewGuid();
            var result = new VaccResultResponse { Id = id };
            _vaccResultServiceMock.Setup(s => s.GetVaccResultByIdAsync(id)).ReturnsAsync(result);

            var actionResult = await _controller.GetVaccResultById(id);
            var okResult = actionResult as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(result, okResult.Value);
        }

        [Test]
        public async Task GetVaccResultById_ReturnsNotFound_WhenNotExists()
        {
            var id = Guid.NewGuid();
            _vaccResultServiceMock.Setup(s => s.GetVaccResultByIdAsync(id)).ThrowsAsync(new KeyNotFoundException());

            var actionResult = await _controller.GetVaccResultById(id);
            Assert.IsInstanceOf<NotFoundResult>(actionResult);
        }
    }
} 