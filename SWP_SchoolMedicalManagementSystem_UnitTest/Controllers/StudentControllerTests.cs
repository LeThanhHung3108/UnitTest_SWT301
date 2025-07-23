using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_API.Controllers;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Service;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.StudentDto;
using Microsoft.AspNetCore.Mvc;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Controllers
{
    [TestFixture]
    public class StudentControllerTests
    {
        private Mock<IStudentService> _studentServiceMock;
        private StudentController _controller;

        [SetUp]
        public void Setup()
        {
            _studentServiceMock = new Mock<IStudentService>();
            _controller = new StudentController(_studentServiceMock.Object);
        }

        [Test]
        public async Task GetAllStudents_ReturnsOk_WithList()
        {
            var list = new List<StudentResponse> { new StudentResponse { Id = Guid.NewGuid() } };
            _studentServiceMock.Setup(s => s.GetAllStudentsAsync()).ReturnsAsync(list);

            var result = await _controller.GetStudents();
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(list, okResult.Value);
        }

        [Test]
        public async Task GetStudentById_ReturnsOk_WhenExists()
        {
            var id = Guid.NewGuid();
            var dto = new StudentResponse { Id = id };
            _studentServiceMock.Setup(s => s.GetStudentByIdAsync(id)).ReturnsAsync(dto);

            var result = await _controller.GetStudentById(id);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(dto, okResult.Value);
        }

        [Test]
        public async Task GetStudentById_ReturnsNotFound_WhenNotExists()
        {
            var id = Guid.NewGuid();
            _studentServiceMock.Setup(s => s.GetStudentByIdAsync(id)).ReturnsAsync((StudentResponse)null);

            var result = await _controller.GetStudentById(id);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
} 