using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_Service.Service;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.MedicationRequestsDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Services
{
    [TestFixture]
    public class MedicationRequestServiceTests
    {
        private Mock<IMedicationReqRepository> _medicationReqRepoMock;
        private Mock<IStudentRepository> _studentRepoMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private Mock<IUserRepository> _userRepoMock;
        private MedicalRequestService _medicationRequestService;

        [SetUp]
        public void Setup()
        {
            _medicationReqRepoMock = new Mock<IMedicationReqRepository>();
            _studentRepoMock = new Mock<IStudentRepository>();
            _mapperMock = new Mock<IMapper>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _userRepoMock = new Mock<IUserRepository>();
            _medicationRequestService = new MedicalRequestService(
                _medicationReqRepoMock.Object,
                _studentRepoMock.Object,
                _mapperMock.Object,
                _httpContextAccessorMock.Object,
                _userRepoMock.Object
            );
        }

        [Test]
        public async Task GetMedicationRequestById_ReturnsRequest_WhenExists()
        {
            var id = Guid.NewGuid();
            var requestEntity = new MedicalRequest { Id = id };
            var requestDto = new MedicationReqResponse { Id = id };
            _medicationReqRepoMock.Setup(r => r.GetMedicationRequestById(id)).ReturnsAsync(requestEntity);
            _mapperMock.Setup(m => m.Map<MedicationReqResponse>(requestEntity)).Returns(requestDto);

            var result = await _medicationRequestService.GetMedicationRequestById(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        public void GetMedicationRequestById_Throws_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _medicationReqRepoMock.Setup(r => r.GetMedicationRequestById(id)).ReturnsAsync((MedicalRequest)null);

            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _medicationRequestService.GetMedicationRequestById(id));
        }
    }
} 