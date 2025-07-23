using SchoolMedicalManagementSystem.Enum;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalDiaryDto
{
    public class MedicalDiaryRequestDto
    {
        public Guid MedicationReqId { get; set; }
        public MedicationStatus? Status { get; set; }
        public string? Description { get; set; }
    }
}
