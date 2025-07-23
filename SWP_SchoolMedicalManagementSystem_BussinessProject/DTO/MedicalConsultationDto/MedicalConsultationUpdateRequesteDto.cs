using SchoolMedicalManagementSystem.Enum;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalConsultationDto
{
    public class MedicalConsultationUpdateRequesteDto
    {
        public DateTime ScheduledDate { get; set; }
        public string? ConsultationNotes { get; set; }
        public ConsultantStatus Status { get; set; }
    }
}
