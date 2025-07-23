namespace SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.PasswordResetDto
{
    public class ResetPasswordRequestDto
    {      
        public string Email { get; set; }
        public string Otp { get; set; }
        public string NewPassword { get; set; }

    }
}
