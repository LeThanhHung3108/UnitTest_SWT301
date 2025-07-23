using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;

namespace SWP_SchoolMedicalManagementSystem_Service.Repository.Interface
{
    public interface IHealthCheckupResultRepository
    {
        Task<List<HealthCheckupResult>> GetAll();
        Task<HealthCheckupResult> GetById(Guid id);
        Task Create(HealthCheckupResult healthCheckupResult);
        Task Update(HealthCheckupResult healthCheckupResult);
        Task Delete(Guid id);
    }
}
