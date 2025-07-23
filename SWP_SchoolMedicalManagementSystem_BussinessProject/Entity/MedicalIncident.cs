using SchoolMedicalManagementSystem.Enum;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Entity
{
    public class MedicalIncident : BaseEntity
    {
        public Guid? StudentId { get; set; }
        public Student? Student { get; set; }
        public Guid? MedicalStaffId { get; set; }
        public User? MedicalStaff { get; set; }
        public IncidentType IncidentType { get; set; }
        public DateTime IncidentDate { get; set; }
        public string? Description { get; set; } 
        public string? ActionsTaken { get; set; }
        public string? Outcome { get; set; }
        public IncidentStatus Status { get; set; }
        public ICollection<MedicalSupplyUsage>? MedicalSupplyUsages { get; set; }

    }
}
