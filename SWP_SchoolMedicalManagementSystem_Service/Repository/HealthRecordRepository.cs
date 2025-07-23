using Microsoft.EntityFrameworkCore;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Context;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;

namespace SWP_SchoolMedicalManagementSystem_Service.Repository.Implementation
{
    public class HealthRecordRepository : IHealthRecordRepository
    {
        private readonly ApplicationDBContext _context;

        public HealthRecordRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        //1. Get all health records
        public async Task<List<HealthRecord>> GetAllHealthRecordAsync()
        {
            return await _context.HealthRecords.AsNoTracking().ToListAsync();
        }

        //2. Get health record by ID
        public async Task<HealthRecord?> GetHealthRecordByIdAsync(Guid healthRecordId)
        {
            var healthRecord = await _context.HealthRecords.FirstOrDefaultAsync(hr => hr.Id == healthRecordId);
            return healthRecord;
        }

        //3. Get health record by student ID
        public async Task<HealthRecord?> GetHealthRecordByStudentIdAsync(Guid studentId)
        {
            var healthRecord = await _context.HealthRecords.FirstOrDefaultAsync(hr => hr.StudentId == studentId);
            return healthRecord;
        }

        //4. Create a new health record
        public async Task CreateHealthRecordAsync(HealthRecord healthRecord)
        {
            await _context.HealthRecords.AddAsync(healthRecord);
            await _context.SaveChangesAsync();
        }

        //5. Update an existing health record
        public async Task UpdateHealthRecordAsync(HealthRecord healthRecord)
        {
            _context.Entry(healthRecord).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }

        //6. Delete a health record
        public async Task DeleteHealthRecordAsync(Guid healthRecordId)
        {
            var healthRecord = await GetHealthRecordByIdAsync(healthRecordId);
            if (healthRecord != null)
            {
                _context.HealthRecords.Remove(healthRecord);
                await _context.SaveChangesAsync();
            }
        }

    }
}