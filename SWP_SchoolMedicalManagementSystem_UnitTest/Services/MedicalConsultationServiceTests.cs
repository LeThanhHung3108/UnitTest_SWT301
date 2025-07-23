using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_Service.Service;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalConsultationDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Services
{
    [TestFixture]
    public class MedicalConsultationServiceTests
    {
        private Mock<IMedicalConsultationRepository> _consultationRepoMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private Mock<IMapper> _mapperMock;
        private MedicalConsultationService _consultationService;

        [SetUp]
        public void Setup()
        {
            _consultationRepoMock = new Mock<IMedicalConsultationRepository>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _mapperMock = new Mock<IMapper>();
            _consultationService = new MedicalConsultationService(_consultationRepoMock.Object, _httpContextAccessorMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetMedicalConsultationByIdAsync_ReturnsConsultation_WhenExists()
        {
            var id = Guid.NewGuid();
            var consultation = new MedicalConsultation { Id = id };
            var consultationDto = new MedicalConsultationResponeDto { Id = id };
            _consultationRepoMock.Setup(r => r.GetMedicalConsultationByIdAsync(id)).ReturnsAsync(consultation);
            _mapperMock.Setup(m => m.Map<MedicalConsultationResponeDto>(consultation)).Returns(consultationDto);

            var result = await _consultationService.GetMedicalConsultationByIdAsync(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        public void GetMedicalConsultationByIdAsync_Throws_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _consultationRepoMock.Setup(r => r.GetMedicalConsultationByIdAsync(id)).ReturnsAsync((MedicalConsultation)null);

            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _consultationService.GetMedicalConsultationByIdAsync(id));
        }
    }
} 