using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.HealthCheckupResultDto;


namespace SWP_SchoolMedicalManagementSystem_Service.Service.Interface
{
    public interface IHealthCheckupResultService
    {
        Task<List<HealthCheckupResponseDto>> GetAll();
        Task<HealthCheckupResponseDto> GetById(Guid id);
        Task CreateHealthCheckupResult(HealthCheckupCreateRequestDto request);
        Task UpdateHealthCheckupResult(Guid requestId,HealthCheckupUpdateRequestDto request);
        Task DeleteHealthCheckupResult(Guid id);
    }
}
