using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_API.Controllers;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.HealthRecordDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Controllers
{
    [TestFixture]
    public class HealthRecordControllerTests
    {
        private Mock<IHealthRecordService> _healthRecordServiceMock;
        private HealthRecordController _controller;

        [SetUp]
        public void Setup()
        {
            _healthRecordServiceMock = new Mock<IHealthRecordService>();
            _controller = new HealthRecordController(_healthRecordServiceMock.Object);
        }

        [Test]
        public async Task GetAllHealthRecords_ReturnsOk_WithList()
        {
            var list = new List<HealthRecordResponse> { new HealthRecordResponse { Id = Guid.NewGuid() } };
            _healthRecordServiceMock.Setup(s => s.GetAllHealthRecordAsync()).ReturnsAsync(list);

            var result = await _controller.GetAllHealthRecord();
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(list, okResult.Value);
        }

        [Test]
        public async Task GetHealthRecordById_ReturnsOk_WhenExists()
        {
            var id = Guid.NewGuid();
            var dto = new HealthRecordResponse { Id = id };
            _healthRecordServiceMock.Setup(s => s.GetHealthRecordByIdAsync(id)).ReturnsAsync(dto);

            var result = await _controller.GetHealthRecordById(id);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(dto, okResult.Value);
        }

        [Test]
        public async Task GetHealthRecordById_ReturnsNotFound_WhenNotExists()
        {
            var id = Guid.NewGuid();
            _healthRecordServiceMock.Setup(s => s.GetHealthRecordByIdAsync(id)).ReturnsAsync((HealthRecordResponse)null);

            var result = await _controller.GetHealthRecordById(id);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
} 