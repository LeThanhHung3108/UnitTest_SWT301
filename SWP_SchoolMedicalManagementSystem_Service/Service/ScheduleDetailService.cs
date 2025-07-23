using AutoMapper;
using Microsoft.AspNetCore.Http;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.ScheduleDetailDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Repository.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;

namespace SWP_SchoolMedicalManagementSystem_Service.Service
{
    public class ScheduleDetailService : IScheduleDetailService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IScheduleDetailRepository _scheduleDetailRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;

        public ScheduleDetailService(IHttpContextAccessor httpContextAccessor, IScheduleDetailRepository scheduleDtailRepository, IScheduleRepository scheduleRepository, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _scheduleDetailRepository = scheduleDtailRepository;
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
        }

        //1. Get all schedule details
        public async Task<List<ScheduleDetailResponse>> GetAllScheduleDetailsAsync()
        {
            var scheduleDetails = await _scheduleDetailRepository.GetAllScheduleDetailsAsync();
            if(scheduleDetails == null || !scheduleDetails.Any())
                throw new KeyNotFoundException("No schedule details found.");
            return _mapper.Map<List<ScheduleDetailResponse>>(scheduleDetails);
        }

        //2. Get schedule detail by ID
        public async Task<ScheduleDetailResponse?> GetScheduleDetailByIdAsync(Guid scheduleDetailId)
        {
            var scheduleDetail = await _scheduleDetailRepository.GetScheduleDetailByIdAsync(scheduleDetailId);
            return _mapper.Map<ScheduleDetailResponse>(scheduleDetail);
        }

        //3. Get schedule details by Schedule ID
        public async Task<List<ScheduleDetailResponse>> GetScheduleDetailsByScheduleIdAsync(Guid scheduleId)
        {
            var scheduleDetails = await _scheduleDetailRepository.GetScheduleDetailsByScheduleIdAsync(scheduleId);
            if (scheduleDetails == null || !scheduleDetails.Any())
                throw new KeyNotFoundException($"No schedule details found for schedule ID {scheduleId}.");
            return _mapper.Map<List<ScheduleDetailResponse>>(scheduleDetails);
        }

        //4. Get schedule details by Student ID
        public async Task<List<ScheduleDetailResponse>> GetScheduleDetailsByStudentIdAsync(Guid studentId)
        {
            var scheduleDetails = await _scheduleDetailRepository.GetScheduleDetailsByStudentIdAsync(studentId);
            if (scheduleDetails == null || !scheduleDetails.Any())
                throw new KeyNotFoundException($"No schedule details found for student ID {studentId}.");
            return _mapper.Map<List<ScheduleDetailResponse>>(scheduleDetails);
        }

        //5. Create a new schedule detail
        public async Task CreateScheduleDetailAsync(ScheduleDetailRequest scheduleDetail)
        {
            var schedule = await _scheduleRepository.GetScheduleByIdAsync(scheduleDetail.ScheduleId.Value);
            if (schedule.ScheduledDate.Date != scheduleDetail.VaccinationDate.Value.Date)
                throw new InvalidOperationException($"VaccinationDate must match the ScheduledDate of the Schedule. ScheduledDate: {schedule.ScheduledDate:yyyy-MM-dd}");

            var newScheduleDetail = _mapper.Map<ScheduleDetail>(scheduleDetail);
            newScheduleDetail.CreatedBy = GetCurrentUsername();
            newScheduleDetail.CreateAt = DateTime.UtcNow;
            newScheduleDetail.UpdateAt = DateTime.UtcNow;
            await _scheduleDetailRepository.CreateScheduleDetailAsync(newScheduleDetail);
        }

        //6. Update an existing schedule detail
        public async Task UpdateScheduleDetailAsync(Guid scheduleDetailId, ScheduleDetailRequest scheduleDetail)
        {
            var existingScheduleDetail = await _scheduleDetailRepository.GetScheduleDetailByIdAsync(scheduleDetailId);
            if (existingScheduleDetail == null)
                throw new KeyNotFoundException($"ScheduleDetail with ID {scheduleDetailId} not found.");

            _mapper.Map(scheduleDetail, existingScheduleDetail);
            existingScheduleDetail.UpdatedBy = GetCurrentUsername();
            existingScheduleDetail.UpdateAt = DateTime.UtcNow;
            await _scheduleDetailRepository.UpdateScheduleDetailAsync(existingScheduleDetail);
        }

        //7. Delete a schedule detail
        public async Task DeleteScheduleDetailAsync(Guid scheduleDetailId)
        {
            var existingScheduleDetail = await _scheduleDetailRepository.GetScheduleDetailByIdAsync(scheduleDetailId);
            if (existingScheduleDetail == null)
                throw new KeyNotFoundException($"ScheduleDetail with ID {scheduleDetailId} not found.");

            await _scheduleDetailRepository.DeleteScheduleDetailAsync(scheduleDetailId);
        }
        private string GetCurrentUsername()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirst("username")?.Value ?? "Unknown User";
        }
    }
}
