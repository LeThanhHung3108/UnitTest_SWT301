using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalIncidentDto;

namespace SWP_SchoolMedicalManagementSystem_Service.Service.Interface
{
    public interface IMedicalIncidentService
    {
        Task<List<IncidentResponseDto>> GetAllIncidentsAsync();
        Task<IncidentResponseDto?> GetIncidentByIdAsync(Guid id);
        Task CreateIncidentAsync(IncidentCreateRequestDto incident);
        Task UpdateIncidentAsync(Guid id, IncidentUpdateRequestDto incident);
        Task DeleteIncidentAsync(Guid id);
        Task<List<IncidentResponseDto>> GetIncidentsByStudentIdAsync(Guid studentId);
    }
}
