namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Entity
{
    public class ScheduleDetail : BaseEntity
    {
        public Guid? StudentId { get; set; }
        public Student? Student { get; set; }
        public Guid? ScheduleId { get; set; }
        public Schedule? Schedule { get; set; }
        public VaccinationResult? VaccinationResult { get; set; }
        public HealthCheckupResult? HealthCheckupResult { get; set; }
        public DateTime VaccinationDate { get; set; }
    }
}
