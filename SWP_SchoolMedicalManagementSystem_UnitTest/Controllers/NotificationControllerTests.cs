using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_API.Controllers;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.NotifyDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Controllers
{
    [TestFixture]
    public class NotificationControllerTests
    {
        private Mock<INotificationService> _notificationServiceMock;
        private NotificationController _controller;

        [SetUp]
        public void Setup()
        {
            _notificationServiceMock = new Mock<INotificationService>();
            _controller = new NotificationController(_notificationServiceMock.Object);
        }

        [Test]
        public async Task GetNotificationById_ReturnsOk_WhenExists()
        {
            var id = Guid.NewGuid();
            var notification = new NotificationResponse { Id = id };
            _notificationServiceMock.Setup(s => s.GetNotificationByIdAsync(id)).ReturnsAsync(notification);

            var result = await _controller.GetNotificationById(id);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(notification, okResult.Value);
        }

        [Test]
        public async Task GetNotificationById_ReturnsNotFound_WhenNotExists()
        {
            var id = Guid.NewGuid();
            _notificationServiceMock.Setup(s => s.GetNotificationByIdAsync(id)).ThrowsAsync(new KeyNotFoundException());

            var result = await _controller.GetNotificationById(id);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
} 