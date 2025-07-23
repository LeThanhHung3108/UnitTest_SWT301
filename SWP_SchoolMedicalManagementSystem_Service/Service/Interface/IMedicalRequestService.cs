using SchoolMedicalManagementSystem.Enum;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.MedicationRequestsDto;

namespace SWP_SchoolMedicalManagementSystem_Service.Service.Interface
{
    public interface IMedicalRequestService
    {
        Task<List<MedicationReqResponse>> GetAllMedicationRequests();
        Task<MedicationReqResponse?> GetMedicationRequestById(Guid medicalReqId);
        Task<List<MedicationReqResponse>> GetMedicationRequestsByStudentId(Guid studentId);
        Task<List<MedicationReqResponse>> GetMedicationRequestsByStatus(RequestStatus status);
        Task CreateMedicationRequest(MedicationReqRequest request);
        Task UpdateMedicationRequest(Guid medicalReqId, MedicationReqRequest request);
        Task DeleteMedicationRequest(Guid medicalReqId);
        Task AccecptMedicationRequest(Guid medicalReqId);
        Task RejectMedicationRequest(Guid medicalReqId);
    }
} 