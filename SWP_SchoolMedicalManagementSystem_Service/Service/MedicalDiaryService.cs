using AutoMapper;
using Microsoft.AspNetCore.Http;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalDiaryDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;

namespace SWP_SchoolMedicalManagementSystem_Service.Service
{
    public class MedicalDiaryService : IMedicalDiaryService
    {
        private readonly IMedicalDiaryRepository _medicalDiaryRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMedicationReqRepository _medicationReqRepository;

        public MedicalDiaryService(IMedicalDiaryRepository medicalDiaryRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IMedicationReqRepository medicationReqRepository)
        {
            _medicalDiaryRepository = medicalDiaryRepository ?? throw new ArgumentNullException(nameof(medicalDiaryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _medicationReqRepository = medicationReqRepository;
        }


        private string GetCurrentUsername()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst("username")!.Value ?? "Unknown";
        }

        public async Task CreateMedicineDiary(MedicalDiaryRequestDto request)
        {

            var medicalRequest = await _medicationReqRepository.GetMedicationRequestById(request.MedicationReqId);
            if (medicalRequest == null)
                throw new KeyNotFoundException($"Medication request with ID {request.MedicationReqId} not found.");

            var medicalDiary = _mapper.Map<MedicalDiary>(request);
            medicalDiary.MedicationReq = medicalRequest;
            medicalDiary.CreateAt = DateTime.UtcNow;
            medicalDiary.CreatedBy = GetCurrentUsername();

            await _medicalDiaryRepository.CreateMedicineDiary(medicalDiary);
        }

        public async Task DeleteMedicineDiary(Guid id)
        {
            var medicalDiary = await _medicalDiaryRepository.GetMedicineDiaryById(id);
            if (medicalDiary == null)
                throw new KeyNotFoundException($"Medical diary with ID {id} not found.");

            await _medicalDiaryRepository.DeleteMedicineDiary(id);
        }

        public async Task<List<MedicalDiaryResponseDto>> GetAllMedicineDiary()
        {
            var medicalDiaries = await _medicalDiaryRepository.GetAllMedicineDiary();
            if (medicalDiaries == null || !medicalDiaries.Any())
                throw new KeyNotFoundException("No medical diaries found.");

            return _mapper.Map<List<MedicalDiaryResponseDto>>(medicalDiaries);
        }

        public async Task<MedicalDiaryResponseDto?> GetMedicineDiaryById(Guid id)
        {
            var medicalDiary = await _medicalDiaryRepository.GetMedicineDiaryById(id);
            if (medicalDiary == null)
                throw new KeyNotFoundException($"Medical diary with ID {id} not found.");

            return _mapper.Map<MedicalDiaryResponseDto>(medicalDiary);
        }

        public async Task<List<MedicalDiaryResponseDto>> GetMedicineDiaryByStudentId(Guid studentId)
        {
            var medicalDiaries = await _medicalDiaryRepository.GetMedicineDiaryByStudentId(studentId);
            if (medicalDiaries == null || !medicalDiaries.Any())
                throw new KeyNotFoundException($"No medical diaries found for student ID {studentId}.");

            return _mapper.Map<List<MedicalDiaryResponseDto>>(medicalDiaries);
        }

        public async Task UpdateMedicineDiary(Guid diaryId, MedicalDiaryRequestDto request)
        {
            var medicalDiary = await _medicalDiaryRepository.GetMedicineDiaryById(diaryId);
            if (medicalDiary == null)
                throw new KeyNotFoundException($"Medical diary with ID {diaryId} not found.");

            var medicalRequest = await _medicationReqRepository.GetMedicationRequestById(request.MedicationReqId);
            if (medicalRequest == null)
                throw new KeyNotFoundException($"Medication request with ID {request.MedicationReqId} not found.");

            _mapper.Map(request, medicalDiary);

            medicalDiary.MedicationReq = medicalRequest;
            medicalDiary.UpdateAt = DateTime.UtcNow;
            medicalDiary.UpdatedBy = GetCurrentUsername();

            await _medicalDiaryRepository.UpdateMedicineDiary(medicalDiary);
        }
    }
}
