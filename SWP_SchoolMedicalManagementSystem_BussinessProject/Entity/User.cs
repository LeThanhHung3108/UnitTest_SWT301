using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolMedicalManagementSystem.Enum;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Entity
{
    public class User : BaseEntity
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public UserRole UserRole { get; set; }
        public string? Image { get; set; }
        public ICollection<Blog>? Blogs { get; set; }
        public ICollection<MedicalConsultation>? MedicalConsultations { get; set; }
        public ICollection<MedicalRequest>? MedicationRequests { get; set; }
        public ICollection<Student>? Students { get; set; }
        public ICollection<Campaign>? Campaigns { get; set; }
        public ICollection<MedicalIncident>? MedicalIncidents { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}