using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_Service.Service;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.ScheduleDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SWP_SchoolMedicalManagementSystem_Repository.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Services
{
    [TestFixture]
    public class ScheduleServiceTests
    {
        private Mock<IScheduleRepository> _scheduleRepoMock;
        private Mock<IScheduleDetailRepository> _scheduleDetailRepoMock;
        private Mock<IStudentRepository> _studentRepoMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private Mock<IConsentFormService> _consentFormServiceMock;
        private ScheduleService _scheduleService;

        [SetUp]
        public void Setup()
        {
            _scheduleRepoMock = new Mock<IScheduleRepository>();
            _scheduleDetailRepoMock = new Mock<IScheduleDetailRepository>();
            _studentRepoMock = new Mock<IStudentRepository>();
            _mapperMock = new Mock<IMapper>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _consentFormServiceMock = new Mock<IConsentFormService>();
            _scheduleService = new ScheduleService(
                _httpContextAccessorMock.Object,
                _scheduleRepoMock.Object,
                _scheduleDetailRepoMock.Object,
                _studentRepoMock.Object,
                _mapperMock.Object,
                _consentFormServiceMock.Object
            );
        }

        [Test]
        public async Task GetScheduleByIdAsync_ReturnsSchedule_WhenExists()
        {
            var id = Guid.NewGuid();
            var schedule = new Schedule { Id = id };
            var scheduleDto = new ScheduleGetByIdResponse { Id = id };
            _scheduleRepoMock.Setup(r => r.GetScheduleByIdAsync(id)).ReturnsAsync(schedule);
            _mapperMock.Setup(m => m.Map<ScheduleGetByIdResponse>(schedule)).Returns(scheduleDto);

            var result = await _scheduleService.GetScheduleByIdAsync(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        public void GetScheduleByIdAsync_Throws_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _scheduleRepoMock.Setup(r => r.GetScheduleByIdAsync(id)).ReturnsAsync((Schedule)null);

            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _scheduleService.GetScheduleByIdAsync(id));
        }
    }
} 