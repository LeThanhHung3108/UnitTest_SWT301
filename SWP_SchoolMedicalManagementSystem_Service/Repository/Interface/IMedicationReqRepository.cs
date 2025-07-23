
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SchoolMedicalManagementSystem.Enum;

namespace SWP_SchoolMedicalManagementSystem_Service.Repository.Interface
{
    public interface IMedicationReqRepository
    {
        Task<List<MedicalRequest>> GetAllMedicationRequests();
        Task<MedicalRequest?> GetMedicationRequestById(Guid medicalReqId);
        Task<List<MedicalRequest>> GetMedicationRequestsByStudentId(Guid studentId);
        Task<List<MedicalRequest>> GetMedicationRequestsByStatus(RequestStatus status);
        Task CreateMedicationRequest(MedicalRequest medicationReq);
        Task UpdateMedicationRequest(MedicalRequest medicationReq);
        Task DeleteMedicationRequest(Guid medicalReqId);
    }
}
