using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_API.Controllers;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalConsultationDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Controllers
{
    [TestFixture]
    public class MedicalConsultationControllerTests
    {
        private Mock<IMedicalConsultationService> _consultationServiceMock;
        private MedicalConsultationController _controller;

        [SetUp]
        public void Setup()
        {
            _consultationServiceMock = new Mock<IMedicalConsultationService>();
            _controller = new MedicalConsultationController(_consultationServiceMock.Object);
        }

        [Test]
        public async Task GetMedicalConsultationById_ReturnsOk_WhenExists()
        {
            var id = Guid.NewGuid();
            var consultation = new MedicalConsultationResponeDto { Id = id };
            _consultationServiceMock.Setup(s => s.GetMedicalConsultationByIdAsync(id)).ReturnsAsync(consultation);

            var result = await _controller.GetById(id);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(consultation, okResult.Value);
        }

        [Test]
        public async Task GetMedicalConsultationById_ReturnsNotFound_WhenNotExists()
        {
            var id = Guid.NewGuid();
            _consultationServiceMock.Setup(s => s.GetMedicalConsultationByIdAsync(id)).ThrowsAsync(new KeyNotFoundException());

            var result = await _controller.GetById(id);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
} 