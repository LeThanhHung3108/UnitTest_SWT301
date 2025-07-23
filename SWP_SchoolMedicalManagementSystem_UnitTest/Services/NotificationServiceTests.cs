using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_Service.Service;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.NotifyDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Services
{
    [TestFixture]
    public class NotificationServiceTests
    {
        private Mock<INotificationRepository> _notificationRepoMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private NotificationService _notificationService;

        [SetUp]
        public void Setup()
        {
            var notificationRepoMock = new Mock<INotificationRepository>();
            var mapperMock = new Mock<AutoMapper.IMapper>();
            var httpContextAccessorMock = new Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
            var userRepoMock = new Mock<IUserRepository>();
            var campaignRepoMock = new Mock<ICampaignRepository>();
            _notificationService = new NotificationService(
                notificationRepoMock.Object,
                mapperMock.Object,
                httpContextAccessorMock.Object,
                userRepoMock.Object,
                campaignRepoMock.Object
            );
        }

        [Test]
        public async Task GetNotificationByIdAsync_ReturnsNotification_WhenExists()
        {
            var id = Guid.NewGuid();
            var notification = new Notification { Id = id };
            var notificationDto = new NotificationResponse { Id = id };
            _notificationRepoMock.Setup(r => r.GetNotificationByIdAsync(id)).ReturnsAsync(notification);
            _mapperMock.Setup(m => m.Map<NotificationResponse>(notification)).Returns(notificationDto);

            var result = await _notificationService.GetNotificationByIdAsync(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        public void GetNotificationByIdAsync_Throws_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _notificationRepoMock.Setup(r => r.GetNotificationByIdAsync(id)).ReturnsAsync((Notification)null);

            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _notificationService.GetNotificationByIdAsync(id));
        }
    }
} 