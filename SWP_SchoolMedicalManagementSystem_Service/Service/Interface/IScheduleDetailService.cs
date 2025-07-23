using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.ScheduleDetailDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_Service.Service.Interface
{
    public interface IScheduleDetailService
    {
        Task<List<ScheduleDetailResponse>> GetAllScheduleDetailsAsync();
        Task<ScheduleDetailResponse?> GetScheduleDetailByIdAsync(Guid scheduleDetailId);
        Task<List<ScheduleDetailResponse>> GetScheduleDetailsByScheduleIdAsync(Guid scheduleId);
        Task<List<ScheduleDetailResponse>> GetScheduleDetailsByStudentIdAsync(Guid studentId);
        Task CreateScheduleDetailAsync(ScheduleDetailRequest scheduleDetail);
        Task UpdateScheduleDetailAsync(Guid scheduleDetailId, ScheduleDetailRequest scheduleDetail);
        Task DeleteScheduleDetailAsync(Guid scheduleDetailId);
    }
}
