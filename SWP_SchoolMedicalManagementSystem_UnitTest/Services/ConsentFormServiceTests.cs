using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_Service.Service;
using SWP_SchoolMedicalManagementSystem_Repository.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccFormDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Services
{
    [TestFixture]
    public class ConsentFormServiceTests
    {
        private Mock<IConsentFormRepository> _consentFormRepoMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private Mock<IMapper> _mapperMock;
        private ConsentFormService _consentFormService;

        [SetUp]
        public void Setup()
        {
            _consentFormRepoMock = new Mock<IConsentFormRepository>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _mapperMock = new Mock<IMapper>();
            _consentFormService = new ConsentFormService(
                _httpContextAccessorMock.Object,
                _consentFormRepoMock.Object,
                _mapperMock.Object
            );
        }

        [Test]
        public async Task GetConsentFormByIdAsync_ReturnsConsentForm_WhenExists()
        {
            var id = Guid.NewGuid();
            var consentForm = new ConsentForm { Id = id };
            var consentFormDto = new ConsentFormResponse { Id = id };
            _consentFormRepoMock.Setup(r => r.GetConsentFormByIdAsync(id)).ReturnsAsync(consentForm);
            _mapperMock.Setup(m => m.Map<ConsentFormResponse>(consentForm)).Returns(consentFormDto);

            var result = await _consentFormService.GetConsentFormByIdAsync(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        public void GetConsentFormByIdAsync_Throws_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _consentFormRepoMock.Setup(r => r.GetConsentFormByIdAsync(id)).ReturnsAsync((ConsentForm)null);

            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _consentFormService.GetConsentFormByIdAsync(id));
        }
    }
} 