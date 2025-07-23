using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_Service.Service;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalDiaryDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Services
{
    [TestFixture]
    public class MedicalDiaryServiceTests
    {
        private Mock<IMedicalDiaryRepository> _medicalDiaryRepoMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private Mock<IMedicationReqRepository> _medicationReqRepoMock;
        private MedicalDiaryService _medicalDiaryService;

        [SetUp]
        public void Setup()
        {
            _medicalDiaryRepoMock = new Mock<IMedicalDiaryRepository>();
            _mapperMock = new Mock<IMapper>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _medicationReqRepoMock = new Mock<IMedicationReqRepository>();
            _medicalDiaryService = new MedicalDiaryService(_medicalDiaryRepoMock.Object, _mapperMock.Object, _httpContextAccessorMock.Object, _medicationReqRepoMock.Object);
        }

        [Test]
        public async Task GetMedicalDiaryByIdAsync_ReturnsDiary_WhenExists()
        {
            var id = Guid.NewGuid();
            var diary = new MedicalDiary { Id = id };
            var diaryDto = new MedicalDiaryResponseDto { Id = id };
            _medicalDiaryRepoMock.Setup(r => r.GetMedicineDiaryById(id)).ReturnsAsync(diary);
            _mapperMock.Setup(m => m.Map<MedicalDiaryResponseDto>(diary)).Returns(diaryDto);

            var result = await _medicalDiaryService.GetMedicineDiaryById(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        public void GetMedicalDiaryByIdAsync_Throws_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _medicalDiaryRepoMock.Setup(r => r.GetMedicineDiaryById(id)).ReturnsAsync((MedicalDiary)null);

            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _medicalDiaryService.GetMedicineDiaryById(id));
        }
    }
} 