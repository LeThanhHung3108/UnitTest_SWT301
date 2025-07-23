using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolMedicalManagementSystem.Enum;
namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Entity
{
    public class MedicalRequest : BaseEntity
    {
        public Guid? StudentId { get; set; }
        public Student? Student { get; set; }
        public string? MedicationName { get; set; }
        public int? Dosage { get; set; }
        public int? NumberOfDayToTake { get; set; }
        public string? Instructions { get; set; }
        public List<string>? ImagesMedicalInvoice { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public RequestStatus Status { get; set; }
        //public string? RejectReason { get; set; } 
        public Guid? MedicalStaffId { get; set; }
        public User? MedicalStaff { get; set; }
        public ICollection<MedicalDiary>? MedicalDiaries { get; set; }
    }
}