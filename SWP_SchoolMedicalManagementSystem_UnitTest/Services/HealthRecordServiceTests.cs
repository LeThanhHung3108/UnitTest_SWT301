using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_Service.Service;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.HealthRecordDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Services
{
    [TestFixture]
    public class HealthRecordServiceTests
    {
        private Mock<IHealthRecordRepository> _healthRecordRepoMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private HealthRecordService _healthRecordService;

        [SetUp]
        public void Setup()
        {
            _healthRecordRepoMock = new Mock<IHealthRecordRepository>();
            _mapperMock = new Mock<IMapper>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _healthRecordService = new HealthRecordService(_httpContextAccessorMock.Object, _healthRecordRepoMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetHealthRecordByIdAsync_ReturnsRecord_WhenExists()
        {
            var id = Guid.NewGuid();
            var record = new HealthRecord { Id = id };
            var recordDto = new HealthRecordResponse { Id = id };
            _healthRecordRepoMock.Setup(r => r.GetHealthRecordByIdAsync(id)).ReturnsAsync(record);
            _mapperMock.Setup(m => m.Map<HealthRecordResponse>(record)).Returns(recordDto);

            var result = await _healthRecordService.GetHealthRecordByIdAsync(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        public void GetHealthRecordByIdAsync_Throws_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _healthRecordRepoMock.Setup(r => r.GetHealthRecordByIdAsync(id)).ReturnsAsync((HealthRecord)null);

            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _healthRecordService.GetHealthRecordByIdAsync(id));
        }
    }
} 