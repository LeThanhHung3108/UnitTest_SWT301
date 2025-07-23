using SchoolMedicalManagementSystem.Enum;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalDiaryDto
{
    public class MedicalDiaryResponseDto
    {
        public Guid Id { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public MedicationStatus? Status { get; set; }
        public string? Description { get; set; }
        public DateTime CreateAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}
