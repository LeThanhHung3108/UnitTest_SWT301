using SchoolMedicalManagementSystem.Enum;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalSupplyUsageDto;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalIncidentDto
{
    public class IncidentCreateRequestDto
    {
        public Guid StudentId { get; set; }
        public IncidentType IncidentType { get; set; }
        public DateTime IncidentDate { get; set; }
        public string? Description { get; set; }
        public string? ActionsTaken { get; set; }
        public string? Outcome { get; set; }
        public IncidentStatus Status { get; set; }
        public List<MedicalSupplyUsageCreateDto>? MedicalSupplyUsage { get; set; } = new List<MedicalSupplyUsageCreateDto>();
    }
}
