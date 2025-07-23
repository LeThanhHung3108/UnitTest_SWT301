using SchoolMedicalManagementSystem.Enum;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Entity
{
    public class Student : BaseEntity
    {
        public Guid? ParentId { get; set; }
        public User? Parent {  get; set; }
        public HealthRecord? HealthRecord { get; set; }
        public string? StudentCode { get; set; }
        public string? FullName { get; set;} 
        public DateTime DateOfBirth { get; set;}
        public Gender Gender { get; set; }      
        public string? Class { get; set; } 
        public string? SchoolYear { get; set; } 
        public string? Image {  get; set; }
        public ICollection<MedicalRequest>? MedicationRequests { get; set; }
        public ICollection<ConsentForm>? ConsentForms { get; set; }
        public ICollection<ScheduleDetail>? ScheduleDetails { get; set; }
        public ICollection<MedicalIncident>? MedicalIncidents { get; set; }
        public ICollection<MedicalConsultation>? MedicalConsultations { get; set; }
        public ICollection<MedicalDiary>? MedicineDiaries { get; set; }
    }
}
