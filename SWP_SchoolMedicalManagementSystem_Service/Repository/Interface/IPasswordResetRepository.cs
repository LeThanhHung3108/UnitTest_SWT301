

using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.PasswordResetDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;

namespace SWP_SchoolMedicalManagementSystem_Service.Repository.Interface
{
    public interface IPasswordResetRepository
    {
        Task Add(PasswordReset passwordReset);
        Task<PasswordReset> GetValidRequest(string email, string otp);
        Task MarkAsUsed(Guid passwordResetId);
    }
}
