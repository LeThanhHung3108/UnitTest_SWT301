using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_API.Controllers;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.ScheduleDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccScheduleDto;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Controllers
{
    [TestFixture]
    public class ScheduleControllerTests
    {
        private Mock<IScheduleService> _scheduleServiceMock;
        private ScheduleController _controller;

        [SetUp]
        public void Setup()
        {
            _scheduleServiceMock = new Mock<IScheduleService>();
            _controller = new ScheduleController(_scheduleServiceMock.Object);
        }

        [Test]
        public async Task GetAllSchedules_ReturnsOk_WithList()
        {
            var list = new List<ScheduleResponse> { new ScheduleResponse { Id = Guid.NewGuid() } };
            _scheduleServiceMock.Setup(s => s.GetAllSchedulesAsync()).ReturnsAsync(list);

            var result = await _controller.GetAllSchedules();
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(list, okResult.Value);
        }

        [Test]
        public async Task GetScheduleById_ReturnsOk_WhenExists()
        {
            var id = Guid.NewGuid();
            var dto = new ScheduleGetByIdResponse { Id = id };
            _scheduleServiceMock.Setup(s => s.GetScheduleByIdAsync(id)).ReturnsAsync(dto);

            var result = await _controller.GetScheduleById(id);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(dto, okResult.Value);
        }

        [Test]
        public async Task GetScheduleById_ReturnsNotFound_WhenNotExists()
        {
            var id = Guid.NewGuid();
            _scheduleServiceMock.Setup(s => s.GetScheduleByIdAsync(id)).ReturnsAsync((ScheduleGetByIdResponse)null);

            var result = await _controller.GetScheduleById(id);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
} 