using Microsoft.EntityFrameworkCore;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Context;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_Service.Repository
{
    public class ScheduleDetailRepository : IScheduleDetailRepository
    {
        private readonly ApplicationDBContext _context;

        public ScheduleDetailRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        //1. Get all schedule details
        public async Task<List<ScheduleDetail>> GetAllScheduleDetailsAsync()
        {
            return await _context.ScheduleDetails
                .Include(sd => sd.Student)
                .Include(sd => sd.Schedule)
                .Include(sd => sd.VaccinationResult)
                .Include(sd => sd.HealthCheckupResult)
                .ToListAsync();
        }

        //2. Get schedule detail by ID
        public async Task<ScheduleDetail?> GetScheduleDetailByIdAsync(Guid scheduleDetailId)
        {
            return await _context.ScheduleDetails
                .Include(sd => sd.Student)
                .Include(sd => sd.Schedule)
                .Include(sd => sd.VaccinationResult)
                .Include(sd => sd.HealthCheckupResult)
                .FirstOrDefaultAsync(sd => sd.Id == scheduleDetailId);
        }

        //3. Get schedule details by schedule ID
        public async Task<List<ScheduleDetail>> GetScheduleDetailsByScheduleIdAsync(Guid scheduleId)
        {
            return await _context.ScheduleDetails
                .Where(sd => sd.ScheduleId == scheduleId)
                .Include(sd => sd.Student)
                .Include(sd => sd.VaccinationResult)
                .Include(sd => sd.HealthCheckupResult)
                .ToListAsync();
        }

        //4. Get schedule details by student ID
        public async Task<List<ScheduleDetail>> GetScheduleDetailsByStudentIdAsync(Guid studentId)
        {
            return await _context.ScheduleDetails
                .Where(sd => sd.StudentId == studentId)
                .Include(sd => sd.Schedule)
                .Include(sd => sd.VaccinationResult)
                .Include(sd => sd.HealthCheckupResult)
                .ToListAsync();
        }

        //5. Create a new schedule detail
        public async Task CreateScheduleDetailAsync(ScheduleDetail scheduleDetail)
        {
            await _context.ScheduleDetails.AddAsync(scheduleDetail);
            await _context.SaveChangesAsync();
        }

        //6. Update an existing schedule detail
        public async Task UpdateScheduleDetailAsync(ScheduleDetail scheduleDetail)
        {
            _context.Entry(scheduleDetail).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        //7. Delete a schedule detail by ID
        public async Task DeleteScheduleDetailAsync(Guid scheduleDetailId)
        {
            var scheduleDetail = await _context.ScheduleDetails.FindAsync(scheduleDetailId);
            if (scheduleDetail != null)
            {
                _context.ScheduleDetails.Remove(scheduleDetail);
                await _context.SaveChangesAsync();
            }
        }
    }
}
