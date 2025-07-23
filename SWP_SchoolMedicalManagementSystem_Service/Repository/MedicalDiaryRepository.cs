using Microsoft.EntityFrameworkCore;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Context;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;

namespace SWP_SchoolMedicalManagementSystem_Service.Repository
{
    public class MedicalDiaryRepository : IMedicalDiaryRepository
    {
        private readonly ApplicationDBContext _context;

        public MedicalDiaryRepository(ApplicationDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateMedicineDiary(MedicalDiary medicineDiary)
        {
            await _context.MedicalDiaries.AddAsync(medicineDiary);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMedicineDiary(Guid id)
        {
            var medicineDiary = await _context.MedicalDiaries.FirstOrDefaultAsync(m => m.Id == id);
            _context.MedicalDiaries.Remove(medicineDiary);
            await _context.SaveChangesAsync();
        }

        public Task<List<MedicalDiary>> GetAllMedicineDiary()
        {
            var medicineDiaries = _context.MedicalDiaries
                .Include(m => m.MedicationReq)
                .ThenInclude(m => m.Student)
                .OrderByDescending(m => m.CreateAt)
                .ToListAsync();
            return medicineDiaries;
        }

        public Task<MedicalDiary?> GetMedicineDiaryById(Guid id)
        {
            var medicineDiary = _context.MedicalDiaries
                .Include(m => m.MedicationReq)
                .ThenInclude(m => m.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            return medicineDiary;
        }

        public Task<List<MedicalDiary>> GetMedicineDiaryByStudentId(Guid studentId)
        {
            var medicineDiaries = _context.MedicalDiaries
                .Include(m => m.MedicationReq)
                .ThenInclude(m => m.Student)
                .Where(m => m.MedicationReq.StudentId == studentId)
                .OrderByDescending(m => m.CreateAt)
                .ToListAsync();
            return medicineDiaries;
        }

        public async Task UpdateMedicineDiary(MedicalDiary medicineDiary)
        {
            _context.Entry(medicineDiary).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
