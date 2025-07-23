using Microsoft.EntityFrameworkCore;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Context;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_Service.Repository
{
    public class MedicalConsultationRepository : IMedicalConsultationRepository
    {
        private readonly ApplicationDBContext _context;

        public MedicalConsultationRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task CreateMedicalConsultationAsync(MedicalConsultation medicalConsultation)
        {
            await _context.MedicalConsultations.AddAsync(medicalConsultation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMedicalConsultationAsync(Guid id)
        {
            var medicalConsultation = await _context.MedicalConsultations.FirstOrDefaultAsync(x => x.Id.Equals(id));
            _context.MedicalConsultations.Remove(medicalConsultation!);
            await _context.SaveChangesAsync();

        }

        public async Task<List<MedicalConsultation>> GetAllMedicalConsultationsAsync()
        {
            var medicalConsultations = await _context.MedicalConsultations.ToListAsync();
            return medicalConsultations;
        }

        public async Task<MedicalConsultation?> GetMedicalConsultationByIdAsync(Guid id)
        {
            var medicalConsultation = await _context.MedicalConsultations.FirstOrDefaultAsync(x => x.Id.Equals(id));
            return medicalConsultation;
        }

        public async Task UpdateMedicalConsultationAsync(MedicalConsultation medicalConsultation)
        {
            _context.Entry(medicalConsultation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
