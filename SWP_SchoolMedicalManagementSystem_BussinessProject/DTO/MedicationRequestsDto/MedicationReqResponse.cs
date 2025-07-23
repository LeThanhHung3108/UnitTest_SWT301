using SchoolMedicalManagementSystem.Enum;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalDiaryDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.UserDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.StudentDto;


namespace SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.MedicationRequestsDto
{
    public class MedicationReqResponse
    {
        public Guid Id { get; set; }
        public string? MedicationName { get; set; }
        public int? Dosage { get; set; }
        public int? NumberOfDayToTake { get; set; }
        public string? Instructions { get; set; }
        public List<string>? ImagesMedicalInvoice { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public RequestStatus Status { get; set; }
        //public string? RejectReason { get; set; }
        public string? StudentCode { get; set; }  
        public string? StudentName { get; set; }
        public Guid? MedicalStaffId { get; set; }
        public string? MedicalStaffName { get; set; }
        public List<MedicalDiaryResponseDto>? MedicalDiaries { get; set; }
    }
}
