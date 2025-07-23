namespace SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.ScheduleDto
{
    public class AssignStudentToScheduleDto
    {
        public Guid ScheduleId { get; set; }
        public List<Guid> StudentIds { get; set; } = [];
    }
}
