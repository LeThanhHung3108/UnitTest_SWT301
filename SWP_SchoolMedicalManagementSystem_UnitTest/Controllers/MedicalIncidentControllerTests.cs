using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_API.Controllers;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalIncidentDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Controllers
{
    [TestFixture]
    public class MedicalIncidentControllerTests
    {
        private Mock<IMedicalIncidentService> _incidentServiceMock;
        private MedicalIncidentController _controller;

        [SetUp]
        public void Setup()
        {
            _incidentServiceMock = new Mock<IMedicalIncidentService>();
            _controller = new MedicalIncidentController(_incidentServiceMock.Object);
        }

        [Test]
        public async Task GetAllIncidents_ReturnsOk_WithList()
        {
            var list = new List<IncidentResponseDto> { new IncidentResponseDto { Id = Guid.NewGuid() } };
            _incidentServiceMock.Setup(s => s.GetAllIncidentsAsync()).ReturnsAsync(list);

            var result = await _controller.GetAllIncidents();
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(list, okResult.Value);
        }

        [Test]
        public async Task GetIncidentById_ReturnsOk_WhenExists()
        {
            var id = Guid.NewGuid();
            var incident = new IncidentResponseDto { Id = id };
            _incidentServiceMock.Setup(s => s.GetIncidentByIdAsync(id)).ReturnsAsync(incident);

            var result = await _controller.GetIncidentById(id);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(incident, okResult.Value);
        }

        [Test]
        public async Task GetIncidentById_ReturnsNotFound_WhenNotExists()
        {
            var id = Guid.NewGuid();
            _incidentServiceMock.Setup(s => s.GetIncidentByIdAsync(id)).ReturnsAsync((IncidentResponseDto)null);
            var result = await _controller.GetIncidentById(id);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task CreateIncident_ReturnsOk_WhenValid()
        {
            var req = new IncidentCreateRequestDto();
            _incidentServiceMock.Setup(s => s.CreateIncidentAsync(req)).Returns(Task.CompletedTask);
            var result = await _controller.CreateIncident(req);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void CreateIncident_Throws_WhenStudentNotFound()
        {
            var req = new IncidentCreateRequestDto();
            _incidentServiceMock.Setup(s => s.CreateIncidentAsync(req)).Throws(new KeyNotFoundException());
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _controller.CreateIncident(req));
        }

        [Test]
        public async Task UpdateIncident_ReturnsOk_WhenValid()
        {
            var id = Guid.NewGuid();
            var req = new IncidentUpdateRequestDto();
            _incidentServiceMock.Setup(s => s.UpdateIncidentAsync(id, req)).Returns(Task.CompletedTask);
            var result = await _controller.UpdateIncident(id, req);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void UpdateIncident_Throws_WhenNotFound()
        {
            var id = Guid.NewGuid();
            var req = new IncidentUpdateRequestDto();
            _incidentServiceMock.Setup(s => s.UpdateIncidentAsync(id, req)).Throws(new KeyNotFoundException());
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _controller.UpdateIncident(id, req));
        }

        [Test]
        public async Task DeleteIncident_ReturnsOk_WhenValid()
        {
            var id = Guid.NewGuid();
            _incidentServiceMock.Setup(s => s.DeleteIncidentAsync(id)).Returns(Task.CompletedTask);
            var result = await _controller.DeleteIncident(id);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void DeleteIncident_Throws_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _incidentServiceMock.Setup(s => s.DeleteIncidentAsync(id)).Throws(new KeyNotFoundException());
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _controller.DeleteIncident(id));
        }

        [Test]
        public async Task GetAllIncidents_ReturnsNotFound_WhenEmpty()
        {
            _incidentServiceMock.Setup(s => s.GetAllIncidentsAsync()).ReturnsAsync((List<IncidentResponseDto>)null);
            var result = await _controller.GetAllIncidents();
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task GetIncidentsByStudentId_ReturnsOk_WhenExists()
        {
            var studentId = Guid.NewGuid();
            var list = new List<IncidentResponseDto> { new IncidentResponseDto { Id = Guid.NewGuid() } };
            _incidentServiceMock.Setup(s => s.GetIncidentsByStudentIdAsync(studentId)).ReturnsAsync(list);
            var result = await _controller.GetIncidentsByStudentId(studentId);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(list, okResult.Value);
        }

        [Test]
        public async Task GetIncidentsByStudentId_ReturnsNotFound_WhenNull()
        {
            var studentId = Guid.NewGuid();
            _incidentServiceMock.Setup(s => s.GetIncidentsByStudentIdAsync(studentId)).ReturnsAsync((List<IncidentResponseDto>)null);
            var result = await _controller.GetIncidentsByStudentId(studentId);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task GetIncidentsByStudentId_ReturnsNotFound_WhenEmpty()
        {
            var studentId = Guid.NewGuid();
            _incidentServiceMock.Setup(s => s.GetIncidentsByStudentIdAsync(studentId)).ReturnsAsync(new List<IncidentResponseDto>());
            var result = await _controller.GetIncidentsByStudentId(studentId);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
} 