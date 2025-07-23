using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_API.Controllers;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.UserDto;
using Microsoft.AspNetCore.Mvc;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<IUserService> _userServiceMock;
        private UserController _controller;

        [SetUp]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
            _controller = new UserController(_userServiceMock.Object);
        }

        [Test]
        public async Task GetAllUsers_ReturnsOk_WithUserList()
        {
            var users = new List<UserResponseDto> { new UserResponseDto { Username = "test" } };
            _userServiceMock.Setup(s => s.GetAllUsersAsync()).ReturnsAsync(users);

            var result = await _controller.GetAllUsers();
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(users, okResult.Value);
        }

        [Test]
        public async Task GetUserById_ReturnsOkResult_WhenUserExists()
        {
            var userId = Guid.NewGuid();
            var user = new UserResponseDto { Id = userId.ToString(), Username = "test" };
            _userServiceMock.Setup(s => s.GetUserByIdAsync(userId)).ReturnsAsync(user);

            var result = await _controller.GetUserById(userId);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(user, okResult.Value);
        }

        [Test]
        public async Task GetUserById_ReturnsNotFound_WhenUserNotExists()
        {
            var userId = Guid.NewGuid();
            _userServiceMock.Setup(s => s.GetUserByIdAsync(userId)).ReturnsAsync((UserResponseDto)null);

            var result = await _controller.GetUserById(userId);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task CreateUser_ReturnsBadRequest_WhenRequestIsNull()
        {
            var result = await _controller.CreateUser(null);
            var badRequest = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequest);
            Assert.AreEqual(400, badRequest.StatusCode);
        }
    }
} 