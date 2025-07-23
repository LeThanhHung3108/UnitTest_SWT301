using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_API.Controllers;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalSupplierDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SWP_SchoolMedicalManagementSystem_Service.IService;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Controllers
{
    [TestFixture]
    public class MedicalSupplierControllerTests
    {
        private Mock<IMedicalSupplierService> _supplierServiceMock;
        private MedicalSupplierController _controller;

        [SetUp]
        public void Setup()
        {
            _supplierServiceMock = new Mock<IMedicalSupplierService>();
            _controller = new MedicalSupplierController(_supplierServiceMock.Object);
        }

        [Test]
        public async Task GetSupplierById_ReturnsOk_WhenExists()
        {
            var id = Guid.NewGuid();
            var supplier = new SupplierResponseDto { Id = id };
            _supplierServiceMock.Setup(s => s.GetSupplierByIdAsync(id)).ReturnsAsync(supplier);

            var result = await _controller.GetSupplierById(id);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(supplier, okResult.Value);
        }

        [Test]
        public async Task GetSupplierById_ReturnsNotFound_WhenNotExists()
        {
            var id = Guid.NewGuid();
            _supplierServiceMock.Setup(s => s.GetSupplierByIdAsync(id)).ThrowsAsync(new KeyNotFoundException());

            var result = await _controller.GetSupplierById(id);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
} 