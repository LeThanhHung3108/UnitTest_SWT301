using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;

namespace SWP_SchoolMedicalManagementSystem_Service.Repository.Interface
{
    public interface IMedicalIncidentRepository
    {
        Task<List<MedicalIncident>> GetAllIncidentsAsync();
        Task<MedicalIncident?> GetIncidentByIdAsync(Guid id);
        Task CreateIncidentAsync(MedicalIncident incident);
        Task UpdateIncidentAsync(MedicalIncident incident);
        Task DeleteIncidentAsync(Guid id);
        Task<List<MedicalIncident>> GetIncidentsByStudentIdAsync(Guid studentId);
    }
}
