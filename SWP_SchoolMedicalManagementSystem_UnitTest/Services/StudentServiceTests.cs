using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Service;
using SWP_SchoolMedicalManagementSystem_Service.Service;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.StudentDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Services
{
    [TestFixture]
    public class StudentServiceTests
    {
        private Mock<IStudentRepository> _studentRepoMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private StudentService _studentService;

        [SetUp]
        public void Setup()
        {
            _studentRepoMock = new Mock<IStudentRepository>();
            _mapperMock = new Mock<IMapper>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _studentService = new StudentService(_httpContextAccessorMock.Object, _studentRepoMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetStudentByIdAsync_ReturnsStudent_WhenExists()
        {
            var id = Guid.NewGuid();
            var student = new Student { Id = id, StudentCode = "S001" };
            var studentDto = new StudentResponse { Id = id, StudentCode = "S001" };
            _studentRepoMock.Setup(r => r.GetStudentByIdAsync(id)).ReturnsAsync(student);
            _mapperMock.Setup(m => m.Map<StudentResponse>(student)).Returns(studentDto);

            var result = await _studentService.GetStudentByIdAsync(id);

            Assert.IsNotNull(result);
            Assert.AreEqual("S001", result.StudentCode);
        }

        [Test]
        public void GetStudentByIdAsync_Throws_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _studentRepoMock.Setup(r => r.GetStudentByIdAsync(id)).ReturnsAsync((Student)null);

            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _studentService.GetStudentByIdAsync(id));
        }
    }
} 