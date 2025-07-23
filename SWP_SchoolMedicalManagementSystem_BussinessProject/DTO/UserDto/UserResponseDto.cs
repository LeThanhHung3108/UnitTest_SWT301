using SchoolMedicalManagementSystem.Enum;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.StudentDto;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.UserDto
{
    public class UserResponseDto
    {
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public UserRole? UserRole { get; set; }
        public string? Image { get; set; }
        public List<StudentResponse>? Students { get; set; }
    }
}
