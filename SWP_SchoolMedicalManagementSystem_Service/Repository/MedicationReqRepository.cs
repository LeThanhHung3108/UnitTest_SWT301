using Microsoft.EntityFrameworkCore;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Context;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using SchoolMedicalManagementSystem.Enum;

namespace SWP_SchoolMedicalManagementSystem_Service.Repository
{
    public class MedicationReqRepository : IMedicationReqRepository
    {
        private readonly ApplicationDBContext _context;

        public MedicationReqRepository(ApplicationDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        //1. Get all medication requests
        public async Task<List<MedicalRequest>> GetAllMedicationRequests()
        {
            return await _context.MedicationRequests
                .Include(mr => mr.Student)
                .Include(mr => mr.MedicalDiaries)
                .Include(mr => mr.MedicalStaff)
                .AsNoTracking()
                .ToListAsync();
        }

        //2. Get medication request by ID
        public async Task<MedicalRequest?> GetMedicationRequestById(Guid medicalReqId)
        {
            return await _context.MedicationRequests
                .Include(mr => mr.Student)
                .Include(mr => mr.MedicalStaff)
                .Include(mr => mr.MedicalDiaries)
                .FirstOrDefaultAsync(mr => mr.Id == medicalReqId);
        }

        //3. Get medication requests by Student ID
        public async Task<List<MedicalRequest>> GetMedicationRequestsByStudentId(Guid studentId)
        {
            return await _context.MedicationRequests
                .Include(mr => mr.Student)
                .Include(mr => mr.MedicalStaff)
                .Include(mr => mr.MedicalDiaries)
                .Where(mr => mr.StudentId == studentId)
                .AsNoTracking()
                .ToListAsync();
        }

        //4. Get medication requests by status
        public async Task<List<MedicalRequest>> GetMedicationRequestsByStatus(RequestStatus status)
        {
            return await _context.MedicationRequests
                .Include(mr => mr.Student)
                .Include(mr => mr.MedicalStaff)
                .Include(mr => mr.MedicalDiaries)
                .Where(mr => mr.Status == status)
                .AsNoTracking()
                .ToListAsync();
        }

        //5. Create a new medication request
        public async Task CreateMedicationRequest(MedicalRequest medicationReq)
        {

            await _context.MedicationRequests.AddAsync(medicationReq);
            await _context.SaveChangesAsync();
        }


        //6. Update an existing medication request
        public async Task UpdateMedicationRequest(MedicalRequest medicationReq)
        {
                _context.Entry(medicationReq).State = EntityState.Modified;
                await _context.SaveChangesAsync();
        }

        //7.Delete a medication request
        public async Task DeleteMedicationRequest(Guid medicalReqId)
        {
            var medicationRequest = await GetMedicationRequestById(medicalReqId);

            if (medicationRequest != null)
            {
                _context.MedicationRequests.Remove(medicationRequest);
                await _context.SaveChangesAsync();
            }
        }
    }
}
