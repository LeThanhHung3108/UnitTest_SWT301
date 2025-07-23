using Microsoft.EntityFrameworkCore;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Context;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Repository.Repository.Interface;

namespace SWP_SchoolMedicalManagementSystem_Repository.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly ApplicationDBContext _context;

        public ScheduleRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        //1. Get all schedules
        public async Task<List<Schedule>> GetAllSchedulesAsync()
        {
            return await _context.Schedules
                .Include(vs => vs.Campaign)
                .Include(vs => vs.ScheduleDetails)
                .ToListAsync();
        }

        //2. Get schedule by ID
        public async Task<Schedule> GetScheduleByIdAsync(Guid scheduleId)
        {
            return await _context.Schedules
                .Include(vs => vs.Campaign)
                .Include(vs => vs.ScheduleDetails)
                .FirstOrDefaultAsync(vs => vs.Id == scheduleId);
        }

        //3. Get schedules by Campaign ID
        public async Task<List<Schedule>> GetSchedulesByCampaignIdAsync(Guid campaignId)
        {
            return await _context.Schedules
                .Include(vs => vs.Campaign)
                .Include(vs => vs.ScheduleDetails)
                .Where(vs => vs.CampaignId == campaignId)
                .ToListAsync();
        }

        //4. Create a new schedule
        public async Task CreateScheduleAsync(Schedule schedule)
        {
            await _context.Schedules.AddAsync(schedule);
            await _context.SaveChangesAsync();
        }

        //5. Update an existing schedule
        public async Task UpdateScheduleAsync(Schedule schedule)
        {
            _context.Entry(schedule).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        //6. Delete a schedule
        public async Task DeleteScheduleAsync(Guid scheduleId)
        {
            var schedule = await GetScheduleByIdAsync(scheduleId);

            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
                await _context.SaveChangesAsync();
            }
            
        }
    }
}