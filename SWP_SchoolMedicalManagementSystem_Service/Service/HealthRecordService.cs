using AutoMapper;
using Microsoft.AspNetCore.Http;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.HealthRecordDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;


namespace SWP_SchoolMedicalManagementSystem_Service.Service
{
    public class HealthRecordService : IHealthRecordService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHealthRecordRepository _healthRecordRepository;
        private readonly IMapper _mapper;

        public HealthRecordService(IHttpContextAccessor httpContextAccessor, IHealthRecordRepository healthRecordRepository, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _healthRecordRepository = healthRecordRepository;
            _mapper = mapper;
        }
        //1. Get all health records
        public async Task<List<HealthRecordResponse>> GetAllHealthRecordAsync()
        {
            var healthRecords = await _healthRecordRepository.GetAllHealthRecordAsync();
            if(healthRecords == null || !healthRecords.Any())
                throw new KeyNotFoundException("No health records found.");
            return _mapper.Map<List<HealthRecordResponse>>(healthRecords);
        }

        //2. Get health record by ID
        public async Task<HealthRecordResponse?> GetHealthRecordByIdAsync(Guid healthRecordId)
        {
            var healthRecord = await _healthRecordRepository.GetHealthRecordByIdAsync(healthRecordId);
            if (healthRecord == null)
                throw new KeyNotFoundException($"Health record with ID {healthRecordId} not found.");
            return _mapper?.Map<HealthRecordResponse>(healthRecord);
        }

        //3. Get health record by student ID
        public async Task<HealthRecordResponse?> GetHealthRecordByStudentIdAsync(Guid studentId)
        {
            var healthRecord = await _healthRecordRepository.GetHealthRecordByStudentIdAsync(studentId);
            if (healthRecord == null)
                throw new KeyNotFoundException($"Health record for student ID {studentId} not found.");
            return _mapper?.Map<HealthRecordResponse>(healthRecord);
        }

        //4. Create a new health record
        public async Task CreateHealthRecordAsync(HealthRecordRequest healthRecord)
        {
            try
            {
                var newHealthRecord = _mapper.Map<HealthRecord>(healthRecord);
                newHealthRecord.CreatedBy = GetCurrentUsername();
                newHealthRecord.CreateAt = DateTime.UtcNow;
                await _healthRecordRepository.CreateHealthRecordAsync(newHealthRecord);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the health record.", ex);
            }
        }

        //5. Update an existing health record
        public async Task UpdateHealthRecordAsync(Guid healthRecordId, HealthRecordRequest healthRecord)
        {
          var existingRecord = await _healthRecordRepository.GetHealthRecordByIdAsync(healthRecordId);
          if (existingRecord == null)
               throw new KeyNotFoundException($"Health record with ID {healthRecordId} not found.");

          _mapper.Map(healthRecord, existingRecord);
            existingRecord.Id = healthRecordId;
            existingRecord.UpdatedBy = GetCurrentUsername();
            existingRecord.UpdateAt = DateTime.UtcNow;
          await _healthRecordRepository.UpdateHealthRecordAsync(existingRecord);
        }

        //6. Delete a health record
        public async Task DeleteHealthRecordAsync(Guid healthRecordId)
        {
            var existingRecord = await _healthRecordRepository.GetHealthRecordByIdAsync(healthRecordId);
            if (existingRecord == null)
                throw new KeyNotFoundException($"Health record with ID {healthRecordId} not found.");
            await _healthRecordRepository.DeleteHealthRecordAsync(healthRecordId);
        }

        //7. Get current username from HTTP context
        private string GetCurrentUsername()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirst("username")?.Value ?? "Unknown User";
        }
    }
}
