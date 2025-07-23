using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_API.Controllers;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.MedicationRequestsDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Controllers
{
    [TestFixture]
    public class MedicationRequestControllerTests
    {
        private Mock<IMedicalRequestService> _medicationRequestServiceMock;
        private MedicationRequestController _controller;

        [SetUp]
        public void Setup()
        {
            _medicationRequestServiceMock = new Mock<IMedicalRequestService>();
            _controller = new MedicationRequestController(_medicationRequestServiceMock.Object);
        }

        [Test]
        public async Task GetAllMedicationRequests_ReturnsOk_WithList()
        {
            var list = new List<MedicationReqResponse> { new MedicationReqResponse { Id = Guid.NewGuid() } };
            _medicationRequestServiceMock.Setup(s => s.GetAllMedicationRequests()).ReturnsAsync(list);

            var result = await _controller.GetAllMedicationRequests();
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(list, okResult.Value);
        }

        [Test]
        public async Task GetMedicationRequestById_ReturnsOk_WhenExists()
        {
            var id = Guid.NewGuid();
            var req = new MedicationReqResponse { Id = id };
            _medicationRequestServiceMock.Setup(s => s.GetMedicationRequestById(id)).ReturnsAsync(req);

            var result = await _controller.GetMedicationRequestById(id);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(req, okResult.Value);
        }

        [Test]
        public async Task GetMedicationRequestById_ReturnsNotFound_WhenNotExists()
        {
            var id = Guid.NewGuid();
            _medicationRequestServiceMock.Setup(s => s.GetMedicationRequestById(id)).ReturnsAsync((MedicationReqResponse)null);

            var result = await _controller.GetMedicationRequestById(id);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task CreateMedicationRequest_ReturnsOk_WhenValid()
        {
            var req = new MedicationReqRequest();
            _medicationRequestServiceMock.Setup(s => s.CreateMedicationRequest(req)).Returns(Task.CompletedTask);

            var result = await _controller.CreateMedicationRequest(req);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task CreateMedicationRequest_ReturnsBadRequest_WhenNull()
        {
            var result = await _controller.CreateMedicationRequest(null);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
} 