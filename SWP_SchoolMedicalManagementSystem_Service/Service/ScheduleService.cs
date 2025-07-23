using AutoMapper;
using Microsoft.AspNetCore.Http;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.ScheduleDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccScheduleDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Repository.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;

namespace SWP_SchoolMedicalManagementSystem_Service.Service
{
    public class ScheduleService : IScheduleService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IScheduleDetailRepository _scheduleDetailRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly IConsentFormService _consentFormService;

        public ScheduleService(
            IHttpContextAccessor httpContextAccessor,
            IScheduleRepository scheduleRepository,
            IScheduleDetailRepository scheduleDetailRepository,
            IStudentRepository studentRepository,
            IMapper mapper,
            IConsentFormService consentFormService)
        {
            _httpContextAccessor = httpContextAccessor;
            _scheduleRepository = scheduleRepository;
            _scheduleDetailRepository = scheduleDetailRepository;
            _studentRepository = studentRepository;
            _mapper = mapper;
            _consentFormService = consentFormService;
        }

        //1. Get all schedules
        public async Task<List<ScheduleResponse>> GetAllSchedulesAsync()
        {
            var schedules = await _scheduleRepository.GetAllSchedulesAsync();
            if (schedules == null || !schedules.Any())
                throw new KeyNotFoundException("No schedules found.");
            return _mapper.Map<List<ScheduleResponse>>(schedules);
        }

        //2. Get schedule by ID
        public async Task<ScheduleGetByIdResponse?> GetScheduleByIdAsync(Guid scheduleId)
        {
            var schedules = await _scheduleRepository.GetScheduleByIdAsync(scheduleId);
            if (schedules == null)
                throw new KeyNotFoundException($"Schedule with ID {scheduleId} not found.");
            return _mapper.Map<ScheduleGetByIdResponse>(schedules);
        }

        //3. Get schedules by campaign ID
        public async Task<List<ScheduleResponse>> GetSchedulesByCampaignIdAsync(Guid campaignId)
        {
            var schedules = await _scheduleRepository.GetSchedulesByCampaignIdAsync(campaignId);
            if (schedules == null || !schedules.Any())
                throw new KeyNotFoundException($"No schedules found for campaign ID {campaignId}.");
            return _mapper.Map<List<ScheduleResponse>>(schedules);
        }

        //4. Create a new schedule
        public async Task CreateScheduleAsync(ScheduleCreateDto request)
        {
            try
            {
                var newSchedule = _mapper.Map<Schedule>(request);
                newSchedule.CreatedBy = GetCurrentUsername();
                newSchedule.CreateAt = DateTime.UtcNow;
                await _scheduleRepository.CreateScheduleAsync(newSchedule);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while creating the schedule.", ex);
            }
        }

        //5. Update an existing schedule
        public async Task UpdateScheduleAsync(Guid scheduleId, ScheduleBaseRequest request)
        {
            var existingSchedule = await _scheduleRepository.GetScheduleByIdAsync(scheduleId);
            if (existingSchedule == null)
                throw new KeyNotFoundException($"Schedule with ID {scheduleId} not found.");

            existingSchedule.UpdatedBy = GetCurrentUsername();
            existingSchedule.UpdateAt = DateTime.UtcNow;
            _mapper.Map(request, existingSchedule);
            await _scheduleRepository.UpdateScheduleAsync(existingSchedule);
        }

        //6. Delete a schedule
        public async Task DeleteScheduleAsync(Guid scheduleId)
        {
            var existingSchedule = await _scheduleRepository.GetScheduleByIdAsync(scheduleId);
            if (existingSchedule == null)
                throw new KeyNotFoundException($"Schedule with ID {scheduleId} not found.");
            foreach(var scheduleDetail in existingSchedule.ScheduleDetails ?? new List<ScheduleDetail>())
            {
                await _scheduleDetailRepository.DeleteScheduleDetailAsync(scheduleDetail.Id);
            }
            await _scheduleRepository.DeleteScheduleAsync(scheduleId);
        }

        //7. Get current username from HTTP context
        private string GetCurrentUsername()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirst("username")?.Value ?? "Unknown User";
        }

        //8. Assign students to a schedule
        public async Task AssignStudentToScheduleAsync(AssignStudentToScheduleDto request)
        {
            var schedule = await _scheduleRepository.GetScheduleByIdAsync(request.ScheduleId);
            if (schedule == null)
            {
                throw new KeyNotFoundException($"Schedule with ID {request.ScheduleId} not found.");
            }
            foreach (var studentId in request.StudentIds)
            {
                var student = await _studentRepository.GetStudentByIdAsync(studentId);
                if(student == null)
                {
                    throw new KeyNotFoundException($"Student with ID {studentId} not found.");
                }

                // Kiểm tra consent form
                var consentForms = await _consentFormService.GetConsentFormsByStudentIdAsync(studentId);
                var validConsent = consentForms.FirstOrDefault(cf => cf.CampaignId == schedule.CampaignId && cf.IsApproved);
                if (validConsent == null)
                {
                    // Bỏ qua nếu không có consent form hợp lệ
                    continue;
                }

                // Kiểm tra học sinh đã có trong schedule chưa
                bool alreadyAssigned = schedule.ScheduleDetails != null &&
                    schedule.ScheduleDetails.Any(sd => sd.StudentId == studentId);
                if (alreadyAssigned)
                {
                    // Bỏ qua nếu đã có
                    continue;
                }

                schedule.ScheduleDetails!.Add(new ScheduleDetail
                {
                    ScheduleId = request.ScheduleId,
                    Schedule = schedule,
                    StudentId = student.Id,
                    Student = student,
                    VaccinationDate = DateTime.UtcNow,
                    CreatedBy = GetCurrentUsername(),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                });

            }
            await _scheduleRepository.UpdateScheduleAsync(schedule);
        }
    }
}
