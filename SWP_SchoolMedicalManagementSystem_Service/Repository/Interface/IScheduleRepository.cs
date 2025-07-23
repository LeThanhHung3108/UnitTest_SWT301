
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_Repository.Repository.Interface
{
    public interface IScheduleRepository
    {
        Task<List<Schedule>> GetAllSchedulesAsync();
        Task<Schedule> GetScheduleByIdAsync(Guid scheduleId);
        Task<List<Schedule>> GetSchedulesByCampaignIdAsync(Guid campaignId);
        Task CreateScheduleAsync(Schedule schedule);
        Task UpdateScheduleAsync(Schedule schedule);
        Task DeleteScheduleAsync(Guid scheduleId);
    }
}