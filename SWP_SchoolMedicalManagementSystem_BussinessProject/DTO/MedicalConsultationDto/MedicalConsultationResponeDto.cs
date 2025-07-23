using SchoolMedicalManagementSystem.Enum;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalConsultationDto
{
    public class MedicalConsultationResponeDto 
    {
        public Guid Id { get; set; }
        public Guid MedicalStaffId { get; set; }
        public string MedicalStaffName { get; set; } = string.Empty;
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public DateTime ScheduledDate { get; set; }
        public string? ConsultationNotes { get; set; }
        public ConsultantStatus Status { get; set; }
    }
}
