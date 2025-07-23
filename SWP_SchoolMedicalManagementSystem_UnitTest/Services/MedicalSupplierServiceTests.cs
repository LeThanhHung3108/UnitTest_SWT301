using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_Service.Service;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalSupplierDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Services
{
    [TestFixture]
    public class MedicalSupplierServiceTests
    {
        private Mock<IMedicalSupplierRepository> _supplierRepoMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private MedicalSupplierService _supplierService;

        [SetUp]
        public void Setup()
        {
            _supplierRepoMock = new Mock<IMedicalSupplierRepository>();
            _mapperMock = new Mock<IMapper>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _supplierService = new MedicalSupplierService(_supplierRepoMock.Object, _mapperMock.Object, _httpContextAccessorMock.Object);
        }

        [Test]
        public async Task GetSupplierByIdAsync_ReturnsSupplier_WhenExists()
        {
            var id = Guid.NewGuid();
            var supplier = new MedicalSupply { Id = id };
            var supplierDto = new SupplierResponseDto { Id = id };
            _supplierRepoMock.Setup(r => r.GetSupplierByIdAsync(id)).ReturnsAsync(supplier);
            _mapperMock.Setup(m => m.Map<SupplierResponseDto>(supplier)).Returns(supplierDto);

            var result = await _supplierService.GetSupplierByIdAsync(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        public void GetSupplierByIdAsync_Throws_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _supplierRepoMock.Setup(r => r.GetSupplierByIdAsync(id)).ReturnsAsync((MedicalSupply)null);

            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _supplierService.GetSupplierByIdAsync(id));
        }
    }
} 