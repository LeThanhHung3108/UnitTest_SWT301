using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_Service.Service;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccResultDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Services
{
    [TestFixture]
    public class VaccResultServiceTests
    {
        private Mock<IVaccResultRepository> _vaccResultRepoMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private VaccResultService _vaccResultService;

        [SetUp]
        public void Setup()
        {
            _vaccResultRepoMock = new Mock<IVaccResultRepository>();
            _mapperMock = new Mock<IMapper>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _vaccResultService = new VaccResultService(_httpContextAccessorMock.Object, _vaccResultRepoMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetVaccResultByIdAsync_ReturnsResult_WhenExists()
        {
            var id = Guid.NewGuid();
            var resultEntity = new VaccinationResult { Id = id };
            var resultDto = new VaccResultResponse { Id = id };
            _vaccResultRepoMock.Setup(r => r.GetVaccResultByIdAsync(id)).ReturnsAsync(resultEntity);
            _mapperMock.Setup(m => m.Map<VaccResultResponse>(resultEntity)).Returns(resultDto);

            var result = await _vaccResultService.GetVaccResultByIdAsync(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        public void GetVaccResultByIdAsync_Throws_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _vaccResultRepoMock.Setup(r => r.GetVaccResultByIdAsync(id)).ReturnsAsync((VaccinationResult)null);

            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _vaccResultService.GetVaccResultByIdAsync(id));
        }
    }
} 