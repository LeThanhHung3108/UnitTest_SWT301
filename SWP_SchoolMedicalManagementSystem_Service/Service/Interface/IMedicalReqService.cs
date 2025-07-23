using SchoolMedicalManagementSystem.Enum;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.MedicationRequestsDto;

namespace SWP_SchoolMedicalManagementSystem_Service.Service.Interface
{
    public interface IMedicalReqService
    {
        Task<List<MedicationReqResponse>> GetAllMedicationRequests();
        Task<MedicationReqResponse?> GetMedicationRequestById(Guid medicalReqId);
        Task<List<MedicationReqResponse>> GetMedicationRequestsByStudentId(Guid studentId);
        Task<List<MedicationReqResponse>> GetMedicationRequestsByStatus(RequestStatus status);
        Task CreateMedicationRequest(MedicationReqRequest medicationReq);
        Task UpdateMedicationRequest(Guid medicalReqId, MedicationReqRequest medicationReq);
        Task DeleteMedicationRequest(Guid medicalReqId);
    }
}
