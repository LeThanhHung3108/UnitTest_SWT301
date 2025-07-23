using SchoolMedicalManagementSystem.Enum;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.HealthRecordDto;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.StudentDto
{
    public class StudentResponse
    {
        public Guid Id { get; set; }
        public string? StudentCode { get; set; }
        public string? FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string? Class { get; set; }
        public string? SchoolYear { get; set; }
        public string? Image { get; set; }
        public HealthRecordResponse? HealthRecord { get; set; }
    }
} 