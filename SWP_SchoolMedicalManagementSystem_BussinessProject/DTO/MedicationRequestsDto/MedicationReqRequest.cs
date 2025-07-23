using SchoolMedicalManagementSystem.Enum;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.MedicationRequestsDto
{
    public class MedicationReqRequest
    {
        public Guid StudentId { get; set; }
        public string? MedicationName { get; set; }
        public int? Dosage { get; set; }
        public int? NumberOfDayToTake { get; set; }
        public string? Instructions { get; set; }
        public List<string>? ImagesMedicalInvoice { get; set; }
        public DateTime? StartDate { get; set; }
        public RequestStatus Status { get; set; }
        public Guid? MedicalStaffId { get; set; }
    }
}
