using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_Service.Repository.Interface
{
    public interface IScheduleDetailRepository
    {
        Task<List<ScheduleDetail>> GetAllScheduleDetailsAsync();
        Task<ScheduleDetail?> GetScheduleDetailByIdAsync(Guid scheduleDetailId);
        Task<List<ScheduleDetail>> GetScheduleDetailsByScheduleIdAsync(Guid scheduleId);
        Task<List<ScheduleDetail>> GetScheduleDetailsByStudentIdAsync(Guid studentId);
        Task CreateScheduleDetailAsync(ScheduleDetail scheduleDetail);
        Task UpdateScheduleDetailAsync(ScheduleDetail scheduleDetail);
        Task DeleteScheduleDetailAsync(Guid scheduleDetailId);
    }
}
