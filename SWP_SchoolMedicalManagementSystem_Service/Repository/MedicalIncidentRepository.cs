using Microsoft.EntityFrameworkCore;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Context;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalIncidentDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;

namespace SWP_SchoolMedicalManagementSystem_Service.Repository
{
    public class MedicalIncidentRepository : IMedicalIncidentRepository
    {
        private readonly ApplicationDBContext _context;

        public MedicalIncidentRepository(ApplicationDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateIncidentAsync(MedicalIncident incident)
        {
            await _context.MedicalIncidents.AddAsync(incident);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIncidentAsync(Guid id)
        {
            var incident = await _context.MedicalIncidents.FirstOrDefaultAsync(i => i.Id == id);
            _context.MedicalIncidents.Remove(incident);
            await _context.SaveChangesAsync();
        }

        public async Task<List<MedicalIncident>> GetAllIncidentsAsync()
        {
            var incidentList = await _context.MedicalIncidents
                .Include(i => i.Student!)
                .Include(i => i.MedicalSupplyUsages!)
                    .ThenInclude(msu => msu.MedicalSupply)
                .AsNoTracking()
                .ToListAsync();
            return incidentList;
        }

        public async Task<MedicalIncident?> GetIncidentByIdAsync(Guid id)
        {
            var incident = await _context.MedicalIncidents
                .Include(i => i.Student!)
                .Include(i => i.MedicalSupplyUsages!)
                    .ThenInclude(msu => msu.MedicalSupply)
                .FirstOrDefaultAsync(i => i.Id == id);
            return incident;
        }

        public async Task<List<MedicalIncident>> GetIncidentsByStudentIdAsync(Guid studentId)
        {
            var incidents = await _context.MedicalIncidents
                .Include(i => i.Student!)
                .Include(i => i.MedicalSupplyUsages!)
                    .ThenInclude(msu => msu.MedicalSupply)
                .Where(i => i.StudentId == studentId)
                .AsNoTracking()
                .ToListAsync();
            return incidents;
        }

        public async Task UpdateIncidentAsync(MedicalIncident incident)
        {
            _context.Entry(incident).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
