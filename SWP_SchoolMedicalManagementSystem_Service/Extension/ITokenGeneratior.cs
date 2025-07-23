

using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.AuthDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;

namespace SWP_SchoolMedicalManagementSystem_Service.Extension
{
    public interface ITokenGeneratior
    {
        Task<AuthResponseDto> GenerateToken(User user, string role);
    }
}
