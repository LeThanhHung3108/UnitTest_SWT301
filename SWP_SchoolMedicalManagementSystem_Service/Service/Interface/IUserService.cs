using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.AuthDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.UserDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.PasswordResetDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;

namespace SWP_SchoolMedicalManagementSystem_Service.Service.Interface
{
    public interface IUserService
    {
        Task<UserResponseDto?> GetUserByIdAsync(Guid userId);
        Task<UserResponseDto?> GetUserByUsernamelAsync(string username);
        Task<UserResponseDto?> GetUserByEmailAsync(string email);
        Task<List<UserResponseDto>> GetAllUsersAsync();
        Task<AuthResponseDto> Login(AuthRequestDto request);
        Task Register(UserRegisterRequestDto request);
        Task UpdateUserAsync(Guid id, UserUpdateRequestDto request);
        Task DeleteUserAsync(Guid userId);
        Task CreateUserAsync(UserCreateRequestDto request);
        Task ForgotPasswordAsync(string email);
        Task<bool> VerifyOtpAsync(string email, string otp);
        Task ResetPasswordAsync(string email, string otp, string newPassword);
    }
}
