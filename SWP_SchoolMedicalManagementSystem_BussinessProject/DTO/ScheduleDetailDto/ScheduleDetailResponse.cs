using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.HealthCheckupResultDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.StudentDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccResultDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.ScheduleDetailDto
{
    public class ScheduleDetailResponse
    {
        public Guid Id { get; set; }
        public Guid ScheduleId { get; set; }
        public StudentResponse? Student { get; set; }
        public VaccResultResponse? VaccinationResult { get; set; }
        public HealthCheckupResponseDto? HealthCheckupResult { get; set; }
        public DateTime VaccinationDate { get; set; }
    }
}
