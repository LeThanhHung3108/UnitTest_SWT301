using Microsoft.EntityFrameworkCore;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Context;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_Service.Repository
{
    public class VaccResultRepository : IVaccResultRepository
    {
        private readonly ApplicationDBContext _context;

        public VaccResultRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<VaccinationResult>> GetAllVaccResultsAsync()
        {
            return await _context.VaccinationResults
                .Include(v => v.ScheduleDetail)
                .Include(v => v.MedicalConsultation)
                .ToListAsync();
        }

        public async Task<VaccinationResult?> GetVaccResultByIdAsync(Guid vaccResultId)
        {
            return await _context.VaccinationResults
                .Include(v => v.ScheduleDetail)
                .Include(v => v.MedicalConsultation)
                .FirstOrDefaultAsync(v => v.Id == vaccResultId);
        }

        public async Task CreateVaccResultAsync(VaccinationResult vaccResult)
        {
            await _context.VaccinationResults.AddAsync(vaccResult);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVaccResultAsync(VaccinationResult vaccResult)
        {
            _context.Entry(vaccResult).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVaccResultAsync(Guid vaccResultId)
        {
            var vaccResult = await GetVaccResultByIdAsync(vaccResultId);
            if (vaccResult != null)
            {
                _context.VaccinationResults.Remove(vaccResult);
                await _context.SaveChangesAsync();
            }
        }
    }
}
