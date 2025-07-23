using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_Service.Service;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.HealthCheckupResultDto;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Services
{
    [TestFixture]
    public class HealthCheckupResultServiceTests
    {
        private Mock<IHealthCheckupResultRepository> _resultRepoMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private HealthCheckupResultService _resultService;

        [SetUp]
        public void Setup()
        {
            _resultRepoMock = new Mock<IHealthCheckupResultRepository>();
            _mapperMock = new Mock<IMapper>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _resultService = new HealthCheckupResultService(_resultRepoMock.Object, _mapperMock.Object, _httpContextAccessorMock.Object);
        }

        [Test]
        public async Task GetHealthCheckupResultByIdAsync_ReturnsResult_WhenExists()
        {
            var id = Guid.NewGuid();
            var resultEntity = new HealthCheckupResult { Id = id };
            var resultDto = new HealthCheckupResponseDto { Id = id };
            _resultRepoMock.Setup(r => r.GetById(id)).ReturnsAsync(resultEntity);
            _mapperMock.Setup(m => m.Map<HealthCheckupResponseDto>(resultEntity)).Returns(resultDto);

            var result = await _resultService.GetById(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        public void GetHealthCheckupResultByIdAsync_Throws_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _resultRepoMock.Setup(r => r.GetById(id)).ReturnsAsync((HealthCheckupResult)null);

            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _resultService.GetById(id));
        }
    }
} 