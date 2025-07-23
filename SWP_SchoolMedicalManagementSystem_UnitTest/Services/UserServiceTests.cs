using NUnit.Framework;
using Moq;
using SWP_SchoolMedicalManagementSystem_Service.Service;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.UserDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Service.Extension;
using AutoMapper;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.PasswordResetDto;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepoMock;
        private Mock<IPasswordResetRepository> _passwordResetRepoMock;
        private Mock<IEmailService> _emailServiceMock;
        private Mock<IMapper> _mapperMock;
        private Mock<ITokenGeneratior> _tokenGeneratorMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _passwordResetRepoMock = new Mock<IPasswordResetRepository>();
            _emailServiceMock = new Mock<IEmailService>();
            _mapperMock = new Mock<IMapper>();
            _tokenGeneratorMock = new Mock<ITokenGeneratior>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _userService = new UserService(
                _httpContextAccessorMock.Object,
                _userRepoMock.Object,
                _mapperMock.Object,
                _tokenGeneratorMock.Object,
                _passwordResetRepoMock.Object,
                _emailServiceMock.Object
            );
        }

        [Test]
        public async Task GetUserByIdAsync_ReturnsUser_WhenUserExists()
        {
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Username = "testuser" };
            var userDto = new UserResponseDto { Id = userId.ToString(), Username = "testuser" };
            _userRepoMock.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<UserResponseDto>(user)).Returns(userDto);

            var result = await _userService.GetUserByIdAsync(userId);

            Assert.IsNotNull(result);
            Assert.AreEqual(userId, result.Id);
        }

        [Test]
        public void GetUserByIdAsync_Throws_WhenUserNotFound()
        {
            var userId = Guid.NewGuid();
            _userRepoMock.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync((User)null);

            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _userService.GetUserByIdAsync(userId));
        }

        [Test]
        public async Task CreateUserAsync_Success()
        {
            var request = new UserCreateRequestDto { Username = "test", Password = "123" };
            var user = new User { Username = "test" };
            _mapperMock.Setup(m => m.Map<User>(request)).Returns(user);
            _userRepoMock.Setup(r => r.AddUserAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
            _httpContextAccessorMock.Setup(x => x.HttpContext.User.Identity.Name).Returns("admin");

            await _userService.CreateUserAsync(request);
            _userRepoMock.Verify(r => r.AddUserAsync(It.Is<User>(u => u.Username == "test")), Times.Once);
        }

        [Test]
        public void DeleteUserAsync_Throws_WhenUserNotFound()
        {
            var userId = Guid.NewGuid();
            _userRepoMock.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync((User)null);

            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _userService.DeleteUserAsync(userId));
        }
    }
} 