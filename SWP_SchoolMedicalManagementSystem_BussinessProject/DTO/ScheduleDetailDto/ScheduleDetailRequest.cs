namespace SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.ScheduleDetailDto
{
    public class ScheduleDetailRequest
    {
        public Guid? StudentId { get; set; }
        public Guid? ScheduleId { get; set; }
        public DateTime? VaccinationDate { get; set; }
    }
}
