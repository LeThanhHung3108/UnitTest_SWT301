using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_Service.Service;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalIncidentDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Services
{
    [TestFixture]
    public class MedicalIncidentServiceTests
    {
        private Mock<IMedicalIncidentRepository> _incidentRepoMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private Mock<IStudentRepository> _studentRepoMock;
        private Mock<IMedicalSupplierRepository> _medicalSupplierRepoMock;
        private MedicalIncidentService _incidentService;

        [SetUp]
        public void Setup()
        {
            _incidentRepoMock = new Mock<IMedicalIncidentRepository>();
            _mapperMock = new Mock<IMapper>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _studentRepoMock = new Mock<IStudentRepository>();
            _medicalSupplierRepoMock = new Mock<IMedicalSupplierRepository>();
            _incidentService = new MedicalIncidentService(
                _incidentRepoMock.Object,
                _mapperMock.Object,
                _httpContextAccessorMock.Object,
                _studentRepoMock.Object,
                _medicalSupplierRepoMock.Object
            );
        }

        [Test]
        public async Task GetMedicalIncidentByIdAsync_ReturnsIncident_WhenExists()
        {
            var id = Guid.NewGuid();
            var incident = new MedicalIncident { Id = id };
            var incidentDto = new IncidentResponseDto { Id = id };
            _incidentRepoMock.Setup(r => r.GetIncidentByIdAsync(id)).ReturnsAsync(incident);
            _mapperMock.Setup(m => m.Map<IncidentResponseDto>(incident)).Returns(incidentDto);

            var result = await _incidentService.GetIncidentByIdAsync(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        public void GetMedicalIncidentByIdAsync_Throws_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _incidentRepoMock.Setup(r => r.GetIncidentByIdAsync(id)).ReturnsAsync((MedicalIncident)null);

            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _incidentService.GetIncidentByIdAsync(id));
        }
    }
} 