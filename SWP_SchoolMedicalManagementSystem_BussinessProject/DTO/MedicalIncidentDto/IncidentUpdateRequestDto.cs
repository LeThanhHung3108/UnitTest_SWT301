using SchoolMedicalManagementSystem.Enum;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalIncidentDto
{
    public class IncidentUpdateRequestDto
    {
        public IncidentType IncidentType { get; set; }
        public DateTime IncidentDate { get; set; }
        public string? Description { get; set; }
        public string? ActionsTaken { get; set; }
        public string? Outcome { get; set; }
        public IncidentStatus Status { get; set; }
        public bool ParentNotified { get; set; }
        public DateTime ParentNotificationDate { get; set; }
    }
}
