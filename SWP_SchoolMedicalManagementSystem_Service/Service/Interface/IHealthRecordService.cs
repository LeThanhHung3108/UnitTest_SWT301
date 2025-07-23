using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.HealthRecordDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_Service.Repository.Interface
{
    public interface IHealthRecordService
    {
        Task<List<HealthRecordResponse>> GetAllHealthRecordAsync();
        Task<HealthRecordResponse?> GetHealthRecordByIdAsync(Guid healthRecordId);
        Task<HealthRecordResponse?> GetHealthRecordByStudentIdAsync(Guid studentId);
        Task CreateHealthRecordAsync(HealthRecordRequest healthRecord);
        Task UpdateHealthRecordAsync(Guid healthRecordId, HealthRecordRequest healthRecord);
        Task DeleteHealthRecordAsync(Guid healthRecordId);
    }
}
