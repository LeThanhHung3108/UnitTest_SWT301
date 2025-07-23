using SchoolMedicalManagementSystem.Enum;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.UserDto
{
    public class UserCreateRequestDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string Password { get; set; } = string.Empty!;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public UserRole UserRole { get; set; }
        public string? Image { get; set; }
    }
}
