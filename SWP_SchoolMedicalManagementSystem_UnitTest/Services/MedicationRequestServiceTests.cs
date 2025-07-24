using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_Service.Service;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.MedicationRequestsDto;
using System.Security.Claims;
using SchoolMedicalManagementSystem.Enum;
using System.Linq;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Services
{
    [TestFixture]
    public class MedicationRequestServiceTests
    {
        private Mock<IMedicationReqRepository> _medicationReqRepoMock;
        private Mock<IStudentRepository> _studentRepoMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private Mock<IUserRepository> _userRepoMock;
        private MedicalRequestService _medicationRequestService;

        [SetUp]
        public void Setup()
        {
            _medicationReqRepoMock = new Mock<IMedicationReqRepository>();
            _studentRepoMock = new Mock<IStudentRepository>();
            _mapperMock = new Mock<IMapper>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _userRepoMock = new Mock<IUserRepository>();
            _medicationRequestService = new MedicalRequestService(
                _medicationReqRepoMock.Object,
                _studentRepoMock.Object,
                _mapperMock.Object,
                _httpContextAccessorMock.Object,
                _userRepoMock.Object
            );
        }

        [Test]
        public async Task CreateMedicationRequest_ShouldCreateRequest_WhenValidDataProvided()
        {
            var studentId = Guid.NewGuid();
            var student = new Student { Id = studentId, FullName = "Test Student" };
            var request = new MedicationReqRequest
            {
                StudentId = studentId,
                MedicationName = "Paracetamol",
                Dosage = 1,
                NumberOfDayToTake = 3,
                Instructions = "Take after meals",
                StartDate = DateTime.Now
            };
            var medicalRequest = new MedicalRequest
            {
                Id = Guid.NewGuid(),
                StudentId = studentId,
                MedicationName = request.MedicationName,
                Dosage = request.Dosage,
                Instructions = request.Instructions,
                StartDate = request.StartDate
            };

            _studentRepoMock.Setup(r => r.GetStudentByIdAsync(studentId))
                .ReturnsAsync(student);
            _mapperMock.Setup(m => m.Map<MedicalRequest>(request))
                .Returns(medicalRequest);
            _medicationReqRepoMock.Setup(r => r.CreateMedicationRequest(It.IsAny<MedicalRequest>()))
                .Returns(Task.CompletedTask);

            await _medicationRequestService.CreateMedicationRequest(request);

            _studentRepoMock.Verify(r => r.GetStudentByIdAsync(studentId), Times.Once);
            _medicationReqRepoMock.Verify(r => r.CreateMedicationRequest(It.IsAny<MedicalRequest>()), Times.Once);
        }

        [Test]
        public void CreateMedicationRequest_ShouldThrowException_WhenStudentNotFound()
        {
            var studentId = Guid.NewGuid();
            var request = new MedicationReqRequest
            {
                StudentId = studentId,
                MedicationName = "Paracetamol"
            };

            _studentRepoMock.Setup(r => r.GetStudentByIdAsync(studentId))
                .ReturnsAsync((Student)null);

            Assert.ThrowsAsync<KeyNotFoundException>(
                async () => await _medicationRequestService.CreateMedicationRequest(request));
            _studentRepoMock.Verify(r => r.GetStudentByIdAsync(studentId), Times.Once);
            _medicationReqRepoMock.Verify(r => r.CreateMedicationRequest(It.IsAny<MedicalRequest>()), Times.Never);
        }

        [Test]
        public async Task GetMedicationRequestsByStudentId_ShouldReturnRequests_WhenStudentHasRequests()
        {
            var studentId = Guid.NewGuid();
            var student = new Student { Id = studentId, FullName = "Test Student" };
            var medicalRequests = new List<MedicalRequest>
            {
                new MedicalRequest { Id = Guid.NewGuid(), StudentId = studentId, MedicationName = "Paracetamol" },
                new MedicalRequest { Id = Guid.NewGuid(), StudentId = studentId, MedicationName = "Ibuprofen" }
            };
            var responseRequests = new List<MedicationReqResponse>
            {
                new MedicationReqResponse { Id = medicalRequests[0].Id, MedicationName = "Paracetamol" },
                new MedicationReqResponse { Id = medicalRequests[1].Id, MedicationName = "Ibuprofen" }
            };

            _studentRepoMock.Setup(r => r.GetStudentByIdAsync(studentId))
                .ReturnsAsync(student);
            _medicationReqRepoMock.Setup(r => r.GetMedicationRequestsByStudentId(studentId))
                .ReturnsAsync(medicalRequests);
            _mapperMock.Setup(m => m.Map<List<MedicationReqResponse>>(medicalRequests))
                .Returns(responseRequests);

            var result = await _medicationRequestService.GetMedicationRequestsByStudentId(studentId);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            _studentRepoMock.Verify(r => r.GetStudentByIdAsync(studentId), Times.Once);
            _medicationReqRepoMock.Verify(r => r.GetMedicationRequestsByStudentId(studentId), Times.Once);
        }

        [Test]
        public async Task UpdateMedicationRequest_ShouldUpdateRequest_WhenValidDataProvided()
        {
            var requestId = Guid.NewGuid();
            var studentId = Guid.NewGuid();
            var existingRequest = new MedicalRequest
            {
                Id = requestId,
                StudentId = studentId,
                MedicationName = "Paracetamol"
            };
            var student = new Student { Id = studentId, FullName = "Test Student" };
            var updateRequest = new MedicationReqRequest
            {
                StudentId = studentId,
                MedicationName = "Ibuprofen"
            };

            _medicationReqRepoMock.Setup(r => r.GetMedicationRequestById(requestId))
                .ReturnsAsync(existingRequest);
            _studentRepoMock.Setup(r => r.GetStudentByIdAsync(studentId))
                .ReturnsAsync(student);
            _medicationReqRepoMock.Setup(r => r.UpdateMedicationRequest(It.IsAny<MedicalRequest>()))
                .Returns(Task.CompletedTask);

            await _medicationRequestService.UpdateMedicationRequest(requestId, updateRequest);

            _medicationReqRepoMock.Verify(r => r.GetMedicationRequestById(requestId), Times.Once);
            _studentRepoMock.Verify(r => r.GetStudentByIdAsync(studentId), Times.Once);
            _medicationReqRepoMock.Verify(r => r.UpdateMedicationRequest(It.IsAny<MedicalRequest>()), Times.Once);
        }

        [Test]
        public async Task GetAllMedicationRequests_ShouldReturnList_WhenExists()
        {
            var requests = new List<MedicalRequest>
            {
                new MedicalRequest { Id = Guid.NewGuid(), MedicationName = "A" },
                new MedicalRequest { Id = Guid.NewGuid(), MedicationName = "B" }
            };

            var responses = requests.Select(r => new MedicationReqResponse { Id = r.Id, MedicationName = r.MedicationName }).ToList();

            _medicationReqRepoMock.Setup(r => r.GetAllMedicationRequests()).ReturnsAsync(requests);
            _mapperMock.Setup(m => m.Map<List<MedicationReqResponse>>(requests)).Returns(responses);

            var result = await _medicationRequestService.GetAllMedicationRequests();

            Assert.AreEqual(2, result.Count);
            _medicationReqRepoMock.Verify(r => r.GetAllMedicationRequests(), Times.Once);
        }

        [Test]
        public void GetAllMedicationRequests_ShouldThrow_WhenEmpty()
        {
            _medicationReqRepoMock.Setup(r => r.GetAllMedicationRequests()).ReturnsAsync(new List<MedicalRequest>());
            Assert.ThrowsAsync<KeyNotFoundException>(() => _medicationRequestService.GetAllMedicationRequests());
        }

        [Test]
        public async Task GetMedicationRequestsByStatus_ShouldReturnList_WhenExists()
        {
            var status = RequestStatus.Pending;
            var requests = new List<MedicalRequest> { new MedicalRequest { Id = Guid.NewGuid(), Status = status } };
            var responses = new List<MedicationReqResponse> { new MedicationReqResponse { Id = requests[0].Id, Status = status } };

            _medicationReqRepoMock.Setup(r => r.GetMedicationRequestsByStatus(status)).ReturnsAsync(requests);
            _mapperMock.Setup(m => m.Map<List<MedicationReqResponse>>(requests)).Returns(responses);

            var result = await _medicationRequestService.GetMedicationRequestsByStatus(status);

            Assert.AreEqual(1, result.Count);
            _medicationReqRepoMock.Verify(r => r.GetMedicationRequestsByStatus(status), Times.Once);
        }

        [Test]
        public void GetMedicationRequestsByStatus_ShouldThrow_WhenEmpty()
        {
            var status = RequestStatus.Pending;
            _medicationReqRepoMock.Setup(r => r.GetMedicationRequestsByStatus(status)).ReturnsAsync(new List<MedicalRequest>());
            Assert.ThrowsAsync<KeyNotFoundException>(() => _medicationRequestService.GetMedicationRequestsByStatus(status));
        }

        [Test]
        public async Task DeleteMedicationRequest_ShouldDelete_WhenExists()
        {
            var id = Guid.NewGuid();
            var req = new MedicalRequest { Id = id };
            _medicationReqRepoMock.Setup(r => r.GetMedicationRequestById(id)).ReturnsAsync(req);
            _medicationReqRepoMock.Setup(r => r.DeleteMedicationRequest(id)).Returns(Task.CompletedTask);

            await _medicationRequestService.DeleteMedicationRequest(id);

            _medicationReqRepoMock.Verify(r => r.DeleteMedicationRequest(id), Times.Once);
        }

        [Test]
        public void DeleteMedicationRequest_ShouldThrow_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _medicationReqRepoMock.Setup(r => r.GetMedicationRequestById(id)).ReturnsAsync((MedicalRequest)null);
            Assert.ThrowsAsync<KeyNotFoundException>(() => _medicationRequestService.DeleteMedicationRequest(id));
        }

        [Test]
        public async Task AccecptMedicationRequest_ShouldUpdateStatus_WhenValid()
        {
            var reqId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, UserRole = UserRole.MedicalStaff };
            var req = new MedicalRequest { Id = reqId, Status = RequestStatus.Pending };

            _httpContextAccessorMock.Setup(x => x.HttpContext.User.FindFirst("userId")).Returns(new Claim("userId", userId.ToString()));
            _userRepoMock.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync(user);
            _medicationReqRepoMock.Setup(r => r.GetMedicationRequestById(reqId)).ReturnsAsync(req);
            _medicationReqRepoMock.Setup(r => r.UpdateMedicationRequest(It.IsAny<MedicalRequest>())).Returns(Task.CompletedTask);

            await _medicationRequestService.AccecptMedicationRequest(reqId);

            _medicationReqRepoMock.Verify(r => r.UpdateMedicationRequest(It.Is<MedicalRequest>(m => m.Status == RequestStatus.Received)), Times.Once);
        }

        [Test]
        public void AccecptMedicationRequest_ShouldThrow_WhenUserNotFound()
        {
            var reqId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            _httpContextAccessorMock.Setup(x => x.HttpContext.User.FindFirst("userId")).Returns(new Claim("userId", userId.ToString()));
            _userRepoMock.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync((User)null);

            Assert.ThrowsAsync<KeyNotFoundException>(() => _medicationRequestService.AccecptMedicationRequest(reqId));
        }

        [Test]
        public void AccecptMedicationRequest_ShouldThrow_WhenUserNotMedicalStaff()
        {
            var reqId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, UserRole = UserRole.Parent };
            _httpContextAccessorMock.Setup(x => x.HttpContext.User.FindFirst("userId")).Returns(new Claim("userId", userId.ToString()));
            _userRepoMock.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync(user);

            Assert.ThrowsAsync<UnauthorizedAccessException>(() => _medicationRequestService.AccecptMedicationRequest(reqId));
        }

        [Test]
        public void AccecptMedicationRequest_ShouldThrow_WhenRequestNotFound()
        {
            var reqId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, UserRole = UserRole.MedicalStaff };
            _httpContextAccessorMock.Setup(x => x.HttpContext.User.FindFirst("userId")).Returns(new Claim("userId", userId.ToString()));
            _userRepoMock.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync(user);
            _medicationReqRepoMock.Setup(r => r.GetMedicationRequestById(reqId)).ReturnsAsync((MedicalRequest)null);

            Assert.ThrowsAsync<KeyNotFoundException>(() => _medicationRequestService.AccecptMedicationRequest(reqId));
        }

        [Test]
        public void AccecptMedicationRequest_ShouldThrow_WhenRequestNotPending()
        {
            var reqId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, UserRole = UserRole.MedicalStaff };
            var req = new MedicalRequest { Id = reqId, Status = RequestStatus.Received };
            _httpContextAccessorMock.Setup(x => x.HttpContext.User.FindFirst("userId")).Returns(new Claim("userId", userId.ToString()));
            _userRepoMock.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync(user);
            _medicationReqRepoMock.Setup(r => r.GetMedicationRequestById(reqId)).ReturnsAsync(req);

            Assert.ThrowsAsync<InvalidOperationException>(() => _medicationRequestService.AccecptMedicationRequest(reqId));
        }

        [Test]
        public async Task RejectMedicationRequest_ShouldUpdateStatus_WhenValid()
        {
            var reqId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, UserRole = UserRole.MedicalStaff };
            var req = new MedicalRequest { Id = reqId, Status = RequestStatus.Pending };

            _httpContextAccessorMock.Setup(x => x.HttpContext.User.FindFirst("userId")).Returns(new Claim("userId", userId.ToString()));
            _userRepoMock.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync(user);
            _medicationReqRepoMock.Setup(r => r.GetMedicationRequestById(reqId)).ReturnsAsync(req);
            _medicationReqRepoMock.Setup(r => r.UpdateMedicationRequest(It.IsAny<MedicalRequest>())).Returns(Task.CompletedTask);

            await _medicationRequestService.RejectMedicationRequest(reqId);

            _medicationReqRepoMock.Verify(r => r.UpdateMedicationRequest(It.Is<MedicalRequest>(m => m.Status == RequestStatus.Returned)), Times.Once);
        }

        [Test]
        public void RejectMedicationRequest_ShouldThrow_WhenUserNotFound()
        {
            var reqId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            _httpContextAccessorMock.Setup(x => x.HttpContext.User.FindFirst("userId")).Returns(new Claim("userId", userId.ToString()));
            _userRepoMock.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync((User)null);

            Assert.ThrowsAsync<KeyNotFoundException>(() => _medicationRequestService.RejectMedicationRequest(reqId));
        }

        [Test]
        public void RejectMedicationRequest_ShouldThrow_WhenUserNotMedicalStaff()
        {
            var reqId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, UserRole = UserRole.Parent };
            _httpContextAccessorMock.Setup(x => x.HttpContext.User.FindFirst("userId")).Returns(new Claim("userId", userId.ToString()));
            _userRepoMock.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync(user);

            Assert.ThrowsAsync<UnauthorizedAccessException>(() => _medicationRequestService.RejectMedicationRequest(reqId));
        }

        [Test]
        public void RejectMedicationRequest_ShouldThrow_WhenRequestNotFound()
        {
            var reqId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, UserRole = UserRole.MedicalStaff };
            _httpContextAccessorMock.Setup(x => x.HttpContext.User.FindFirst("userId")).Returns(new Claim("userId", userId.ToString()));
            _userRepoMock.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync(user);
            _medicationReqRepoMock.Setup(r => r.GetMedicationRequestById(reqId)).ReturnsAsync((MedicalRequest)null);

            Assert.ThrowsAsync<KeyNotFoundException>(() => _medicationRequestService.RejectMedicationRequest(reqId));
        }

        [Test]
        public void RejectMedicationRequest_ShouldThrow_WhenRequestNotPending()
        {
            var reqId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, UserRole = UserRole.MedicalStaff };
            var req = new MedicalRequest { Id = reqId, Status = RequestStatus.Received };
            _httpContextAccessorMock.Setup(x => x.HttpContext.User.FindFirst("userId")).Returns(new Claim("userId", userId.ToString()));
            _userRepoMock.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync(user);
            _medicationReqRepoMock.Setup(r => r.GetMedicationRequestById(reqId)).ReturnsAsync(req);

            Assert.ThrowsAsync<InvalidOperationException>(() => _medicationRequestService.RejectMedicationRequest(reqId));
        }
    }
}