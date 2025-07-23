
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;

namespace SWP_SchoolMedicalManagementSystem_Service.Repository.Interface
{
    public interface IHealthRecordRepository
    {
        Task<List<HealthRecord>> GetAllHealthRecordAsync();
        Task<HealthRecord?> GetHealthRecordByIdAsync(Guid healthRecordId);
        Task<HealthRecord?> GetHealthRecordByStudentIdAsync(Guid studentId);
        Task CreateHealthRecordAsync(HealthRecord healthRecord);
        Task UpdateHealthRecordAsync(HealthRecord healthRecord);
        Task DeleteHealthRecordAsync(Guid HealthRecordId);
    }
}
    
