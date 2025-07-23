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
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalDiaryDto;
using System.Security.Claims;
using System.Linq;
using SchoolMedicalManagementSystem.Enum;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.Services
{
    [TestFixture]
    public class MedicalDiaryServiceTests
    {
        private Mock<IMedicalDiaryRepository> _mockMedicalDiaryRepository;
        private Mock<IMedicationReqRepository> _mockMedicationReqRepository;
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private Mock<IMapper> _mockMapper;
        private MedicalDiaryService _medicalDiaryService;

        [SetUp]
        public void Setup()
        {
            _mockMedicalDiaryRepository = new Mock<IMedicalDiaryRepository>();
            _mockMedicationReqRepository = new Mock<IMedicationReqRepository>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockMapper = new Mock<IMapper>();

            // Setup HTTP context with username claim
            var httpContext = new DefaultHttpContext();
            var claims = new List<Claim>
            {
                new Claim("username", "testuser")
            };
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);

            _medicalDiaryService = new MedicalDiaryService(
                _mockMedicalDiaryRepository.Object,
                _mockMapper.Object,
                _mockHttpContextAccessor.Object,
                _mockMedicationReqRepository.Object
            );
        }

        [Test]
        public async Task GetAllMedicineDiary_ShouldReturnAllDiaries_WhenDiariesExist()
        {
            var diaries = new List<MedicalDiary>
            {
                new MedicalDiary { Id = Guid.NewGuid(), Description = "Diary 1" },
                new MedicalDiary { Id = Guid.NewGuid(), Description = "Diary 2" }
            };

            var responseDiaries = new List<MedicalDiaryResponseDto>
            {
                new MedicalDiaryResponseDto { Id = diaries[0].Id, Description = "Diary 1" },
                new MedicalDiaryResponseDto { Id = diaries[1].Id, Description = "Diary 2" }
            };

            _mockMedicalDiaryRepository.Setup(r => r.GetAllMedicineDiary())
                .ReturnsAsync(diaries);
            _mockMapper.Setup(m => m.Map<List<MedicalDiaryResponseDto>>(diaries))
                .Returns(responseDiaries);

            var result = await _medicalDiaryService.GetAllMedicineDiary();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            _mockMedicalDiaryRepository.Verify(r => r.GetAllMedicineDiary(), Times.Once);
        }

        [Test]
        public void GetAllMedicineDiary_ShouldThrowException_WhenNoDiariesExist()
        {
            _mockMedicalDiaryRepository.Setup(r => r.GetAllMedicineDiary())
                .ReturnsAsync(new List<MedicalDiary>());

            Assert.ThrowsAsync<KeyNotFoundException>(
                async () => await _medicalDiaryService.GetAllMedicineDiary());
            _mockMedicalDiaryRepository.Verify(r => r.GetAllMedicineDiary(), Times.Once);
        }

        [Test]
        public async Task GetMedicineDiaryById_ShouldReturnDiary_WhenDiaryExists()
        {
            var diaryId = Guid.NewGuid();
            var diary = new MedicalDiary
            {
                Id = diaryId,
                Description = "Test diary",
                Status = MedicationStatus.Taken
            };
            var responseDiary = new MedicalDiaryResponseDto
            {
                Id = diaryId,
                Description = "Test diary",
                Status = MedicationStatus.Taken
            };

            _mockMedicalDiaryRepository.Setup(r => r.GetMedicineDiaryById(diaryId))
                .ReturnsAsync(diary);
            _mockMapper.Setup(m => m.Map<MedicalDiaryResponseDto>(diary))
                .Returns(responseDiary);

            var result = await _medicalDiaryService.GetMedicineDiaryById(diaryId);

            Assert.IsNotNull(result);
            Assert.AreEqual(diaryId, result.Id);
            Assert.AreEqual("Test diary", result.Description);
            _mockMedicalDiaryRepository.Verify(r => r.GetMedicineDiaryById(diaryId), Times.Once);
        }

        [Test]
        public void GetMedicineDiaryById_ShouldThrowException_WhenDiaryNotFound()
        {
            var diaryId = Guid.NewGuid();
            _mockMedicalDiaryRepository.Setup(r => r.GetMedicineDiaryById(diaryId))
                .ReturnsAsync((MedicalDiary)null);

            Assert.ThrowsAsync<KeyNotFoundException>(
                async () => await _medicalDiaryService.GetMedicineDiaryById(diaryId));
            _mockMedicalDiaryRepository.Verify(r => r.GetMedicineDiaryById(diaryId), Times.Once);
        }

        [Test]
        public async Task GetMedicineDiaryByStudentId_ShouldReturnDiaries_WhenStudentHasDiaries()
        {
            var studentId = Guid.NewGuid();
            var diaries = new List<MedicalDiary>
            {
                new MedicalDiary
                {
                    Id = Guid.NewGuid(),
                    MedicationReq = new MedicalRequest { Student = new Student { Id = studentId, FullName = "Test Student" } },
                    Status = MedicationStatus.Taken,
                    Description = "Medication taken successfully"
                },
                new MedicalDiary
                {
                    Id = Guid.NewGuid(),
                    MedicationReq = new MedicalRequest { Student = new Student { Id = studentId, FullName = "Test Student" } },
                    Status = MedicationStatus.Taken,
                    Description = "Second dose"
                }
            };

            var responseDiaries = new List<MedicalDiaryResponseDto>
            {
                new MedicalDiaryResponseDto
                {
                    Id = diaries[0].Id,
                    StudentName = "Test Student",
                    Status = MedicationStatus.Taken,
                    Description = "Medication taken successfully"
                },
                new MedicalDiaryResponseDto
                {
                    Id = diaries[1].Id,
                    StudentName = "Test Student",
                    Status = MedicationStatus.Taken,
                    Description = "Second dose"
                }
            };

            _mockMedicalDiaryRepository.Setup(r => r.GetMedicineDiaryByStudentId(studentId))
                .ReturnsAsync(diaries);
            _mockMapper.Setup(m => m.Map<List<MedicalDiaryResponseDto>>(diaries))
                .Returns(responseDiaries);

            var result = await _medicalDiaryService.GetMedicineDiaryByStudentId(studentId);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            _mockMedicalDiaryRepository.Verify(r => r.GetMedicineDiaryByStudentId(studentId), Times.Once);
        }

        [Test]
        public void GetMedicineDiaryByStudentId_ShouldThrowException_WhenStudentHasNoDiaries()
        {
            var studentId = Guid.NewGuid();
            _mockMedicalDiaryRepository.Setup(r => r.GetMedicineDiaryByStudentId(studentId))
                .ReturnsAsync(new List<MedicalDiary>());

            Assert.ThrowsAsync<KeyNotFoundException>(
                async () => await _medicalDiaryService.GetMedicineDiaryByStudentId(studentId));
            _mockMedicalDiaryRepository.Verify(r => r.GetMedicineDiaryByStudentId(studentId), Times.Once);
        }

        [Test]
        public async Task CreateMedicineDiary_ShouldCreateDiary_WhenValidDataProvided()
        {
            var medicationRequestId = Guid.NewGuid();
            var medicationRequest = new MedicalRequest
            {
                Id = medicationRequestId,
                Student = new Student { FullName = "Test Student" },
                MedicationName = "Paracetamol"
            };
            var diaryRequest = new MedicalDiaryRequestDto
            {
                MedicationReqId = medicationRequestId,
                Status = MedicationStatus.Taken,
                Description = "Medication taken successfully"
            };
            var medicalDiary = new MedicalDiary
            {
                MedicationReqId = medicationRequestId,
                Status = MedicationStatus.Taken,
                Description = "Medication taken successfully"
            };

            _mockMedicationReqRepository.Setup(r => r.GetMedicationRequestById(medicationRequestId))
                .ReturnsAsync(medicationRequest);
            _mockMapper.Setup(m => m.Map<MedicalDiary>(diaryRequest))
                .Returns(medicalDiary);
            _mockMedicalDiaryRepository.Setup(r => r.CreateMedicineDiary(It.IsAny<MedicalDiary>()))
                .Returns(Task.CompletedTask);

            await _medicalDiaryService.CreateMedicineDiary(diaryRequest);

            _mockMedicationReqRepository.Verify(r => r.GetMedicationRequestById(medicationRequestId), Times.Once);
            _mockMedicalDiaryRepository.Verify(r => r.CreateMedicineDiary(It.Is<MedicalDiary>(
                d => d.MedicationReq == medicationRequest &&
                     d.Status == MedicationStatus.Taken &&
                     d.CreatedBy == "testuser")), Times.Once);
        }

        [Test]
        public void CreateMedicineDiary_ShouldThrowException_WhenMedicationRequestNotFound()
        {
            var medicationRequestId = Guid.NewGuid();
            var diaryRequest = new MedicalDiaryRequestDto
            {
                MedicationReqId = medicationRequestId,
                Status = MedicationStatus.Taken
            };

            _mockMedicationReqRepository.Setup(r => r.GetMedicationRequestById(medicationRequestId))
                .ReturnsAsync((MedicalRequest)null);

            Assert.ThrowsAsync<KeyNotFoundException>(
                async () => await _medicalDiaryService.CreateMedicineDiary(diaryRequest));

            _mockMedicationReqRepository.Verify(r => r.GetMedicationRequestById(medicationRequestId), Times.Once);
            _mockMedicalDiaryRepository.Verify(r => r.CreateMedicineDiary(It.IsAny<MedicalDiary>()), Times.Never);
        }

        [Test]
        public async Task UpdateMedicineDiary_ShouldUpdateDiary_WhenValidDataProvided()
        {
            var diaryId = Guid.NewGuid();
            var medicationRequestId = Guid.NewGuid();
            var existingDiary = new MedicalDiary
            {
                Id = diaryId,
                MedicationReqId = medicationRequestId,
                Status = MedicationStatus.NotTaken,
                Description = "Not taken yet"
            };
            var medicationRequest = new MedicalRequest
            {
                Id = medicationRequestId,
                Student = new Student { FullName = "Test Student" }
            };
            var updateRequest = new MedicalDiaryRequestDto
            {
                MedicationReqId = medicationRequestId,
                Status = MedicationStatus.Taken,
                Description = "Medication taken successfully"
            };

            _mockMedicalDiaryRepository.Setup(r => r.GetMedicineDiaryById(diaryId))
                .ReturnsAsync(existingDiary);
            _mockMedicationReqRepository.Setup(r => r.GetMedicationRequestById(medicationRequestId))
                .ReturnsAsync(medicationRequest);
            _mockMedicalDiaryRepository.Setup(r => r.UpdateMedicineDiary(It.IsAny<MedicalDiary>()))
                .Returns(Task.CompletedTask);

            await _medicalDiaryService.UpdateMedicineDiary(diaryId, updateRequest);

            _mockMedicalDiaryRepository.Verify(r => r.GetMedicineDiaryById(diaryId), Times.Once);
            _mockMedicationReqRepository.Verify(r => r.GetMedicationRequestById(medicationRequestId), Times.Once);
            _mockMedicalDiaryRepository.Verify(r => r.UpdateMedicineDiary(It.Is<MedicalDiary>(
                d => d.Id == diaryId &&
                     d.MedicationReq == medicationRequest &&
                     d.UpdatedBy == "testuser")), Times.Once);
        }

        [Test]
        public void UpdateMedicineDiary_ShouldThrowException_WhenDiaryNotFound()
        {
            var diaryId = Guid.NewGuid();
            var medicationRequestId = Guid.NewGuid();
            var updateRequest = new MedicalDiaryRequestDto
            {
                MedicationReqId = medicationRequestId,
                Status = MedicationStatus.Taken
            };

            _mockMedicalDiaryRepository.Setup(r => r.GetMedicineDiaryById(diaryId))
                .ReturnsAsync((MedicalDiary)null);

            Assert.ThrowsAsync<KeyNotFoundException>(
                async () => await _medicalDiaryService.UpdateMedicineDiary(diaryId, updateRequest));

            _mockMedicalDiaryRepository.Verify(r => r.GetMedicineDiaryById(diaryId), Times.Once);
            _mockMedicalDiaryRepository.Verify(r => r.UpdateMedicineDiary(It.IsAny<MedicalDiary>()), Times.Never);
        }

        [Test]
        public void UpdateMedicineDiary_ShouldThrowException_WhenMedicationRequestNotFound()
        {
            var diaryId = Guid.NewGuid();
            var medicationRequestId = Guid.NewGuid();
            var existingDiary = new MedicalDiary
            {
                Id = diaryId,
                MedicationReqId = medicationRequestId,
                Status = MedicationStatus.NotTaken
            };
            var updateRequest = new MedicalDiaryRequestDto
            {
                MedicationReqId = medicationRequestId,
                Status = MedicationStatus.Taken
            };

            _mockMedicalDiaryRepository.Setup(r => r.GetMedicineDiaryById(diaryId))
                .ReturnsAsync(existingDiary);
            _mockMedicationReqRepository.Setup(r => r.GetMedicationRequestById(medicationRequestId))
                .ReturnsAsync((MedicalRequest)null);

            Assert.ThrowsAsync<KeyNotFoundException>(
                async () => await _medicalDiaryService.UpdateMedicineDiary(diaryId, updateRequest));

            _mockMedicalDiaryRepository.Verify(r => r.GetMedicineDiaryById(diaryId), Times.Once);
            _mockMedicationReqRepository.Verify(r => r.GetMedicationRequestById(medicationRequestId), Times.Once);
            _mockMedicalDiaryRepository.Verify(r => r.UpdateMedicineDiary(It.IsAny<MedicalDiary>()), Times.Never);
        }

        [Test]
        public async Task DeleteMedicineDiary_ShouldDeleteDiary_WhenDiaryExists()
        {
            var diaryId = Guid.NewGuid();
            var diary = new MedicalDiary
            {
                Id = diaryId,
                Status = MedicationStatus.Taken,
                Description = "Test diary"
            };

            _mockMedicalDiaryRepository.Setup(r => r.GetMedicineDiaryById(diaryId))
                .ReturnsAsync(diary);
            _mockMedicalDiaryRepository.Setup(r => r.DeleteMedicineDiary(diaryId))
                .Returns(Task.CompletedTask);

            await _medicalDiaryService.DeleteMedicineDiary(diaryId);

            _mockMedicalDiaryRepository.Verify(r => r.GetMedicineDiaryById(diaryId), Times.Once);
            _mockMedicalDiaryRepository.Verify(r => r.DeleteMedicineDiary(diaryId), Times.Once);
        }

        [Test]
        public void DeleteMedicineDiary_ShouldThrowException_WhenDiaryNotFound()
        {
            var diaryId = Guid.NewGuid();
            _mockMedicalDiaryRepository.Setup(r => r.GetMedicineDiaryById(diaryId))
                .ReturnsAsync((MedicalDiary)null);

            Assert.ThrowsAsync<KeyNotFoundException>(
                async () => await _medicalDiaryService.DeleteMedicineDiary(diaryId));

            _mockMedicalDiaryRepository.Verify(r => r.GetMedicineDiaryById(diaryId), Times.Once);
            _mockMedicalDiaryRepository.Verify(r => r.DeleteMedicineDiary(diaryId), Times.Never);
        }
    }
}