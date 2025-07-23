using SchoolMedicalManagementSystem.Enum;
using System.ComponentModel.DataAnnotations;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.UserDto
{
    public class UserRegisterRequestDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
    }
}
