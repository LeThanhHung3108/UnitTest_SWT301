using SchoolMedicalManagementSystem.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Entity
{
    public class MedicalDiary : BaseEntity
    {
        public Guid? MedicationReqId { get; set; }
        public MedicalRequest? MedicationReq { get; set; }
        public MedicationStatus? Status { get; set; }
        public string? Description { get; set; }
    }
}