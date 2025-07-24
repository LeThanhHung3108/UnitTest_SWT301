using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Service;
using SWP_SchoolMedicalManagementSystem_Service.Service;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.StudentDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [Test]
        public async Task GetStudentsByParentIdAsync_ShouldReturnStudents_WhenStudentsExist()
        {
            var parentId = Guid.NewGuid();
            var students = new List<Student>
            {
                new Student { Id = Guid.NewGuid(), ParentId = parentId, FullName = "Student 1" },
                new Student { Id = Guid.NewGuid(), ParentId = parentId, FullName = "Student 2" }
            };

            var studentResponses = new List<StudentResponse>
            {
                new StudentResponse { Id = students[0].Id, FullName = "Student 1" },
                new StudentResponse { Id = students[1].Id, FullName = "Student 2" }
            };

            _studentRepoMock.Setup(r => r.GetStudentsByParentIdAsync(parentId))
                .ReturnsAsync(students);
            _mapperMock.Setup(m => m.Map<List<StudentResponse>>(It.IsAny<List<Student>>()))
                .Returns(studentResponses);

            var result = await _studentService.GetStudentsByParentIdAsync(parentId);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            _studentRepoMock.Verify(r => r.GetStudentsByParentIdAsync(parentId), Times.Once);
        }

        [Test]
        public void GetStudentsByParentIdAsync_ShouldThrowException_WhenNoStudentsFound()
        {
            var parentId = Guid.NewGuid();
            _studentRepoMock.Setup(r => r.GetStudentsByParentIdAsync(parentId))
                .ReturnsAsync(new List<Student>());

            Assert.ThrowsAsync<KeyNotFoundException>(
                async () => await _studentService.GetStudentsByParentIdAsync(parentId));
            _studentRepoMock.Verify(r => r.GetStudentsByParentIdAsync(parentId), Times.Once);
        }

        [Test]
        public async Task GetStudentByStudentCodeAsync_ShouldReturnStudent_WhenStudentExists()
        {
            var studentCode = "STD001";
            var student = new Student
            {
                Id = Guid.NewGuid(),
                StudentCode = studentCode,
                FullName = "Test Student"
            };
            var studentResponse = new StudentResponse
            {
                Id = student.Id,
                StudentCode = studentCode,
                FullName = "Test Student"
            };

            _studentRepoMock.Setup(r => r.GetStudentByStudentCodeAsync(studentCode))
                .ReturnsAsync(student);
            _mapperMock.Setup(m => m.Map<StudentResponse>(student))
                .Returns(studentResponse);

            var result = await _studentService.GetStudentByStudentCodeAsync(studentCode);

            Assert.IsNotNull(result);
            Assert.AreEqual(studentCode, result.StudentCode);
            _studentRepoMock.Verify(r => r.GetStudentByStudentCodeAsync(studentCode), Times.Once);
        }

        [Test]
        public void GetStudentByStudentCodeAsync_ShouldThrowException_WhenCodeIsEmpty()
        {
            string studentCode = "";

            Assert.ThrowsAsync<ArgumentException>(
                async () => await _studentService.GetStudentByStudentCodeAsync(studentCode));
        }
    }
}