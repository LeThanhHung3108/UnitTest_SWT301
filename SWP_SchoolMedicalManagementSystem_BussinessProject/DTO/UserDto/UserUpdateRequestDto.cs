using SchoolMedicalManagementSystem.Enum;
namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.UserDto
{
    public class UserUpdateRequestDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public UserRole UserRole { get; set; }
        public string? Image { get; set; }
    }
}
