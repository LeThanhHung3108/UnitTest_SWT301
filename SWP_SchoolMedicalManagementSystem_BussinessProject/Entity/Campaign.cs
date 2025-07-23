using SchoolMedicalManagementSystem.Enum;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Entity
{
    public class Campaign : BaseEntity
    {
        public string? Name { get; set; } 
        public string? Description { get; set; } // chứa thuốc gì, vaccine gì và các thông tin cơ bản về chiến dịch 
        public CampaignStatus? Status { get; set; }
        public CampaignType? Type { get; set; }
        public ICollection<User>? MedicalStaffs { get; set; }
        public ICollection<Schedule>? Schedules { get; set; }
        public ICollection<ConsentForm>? ConsentForms { get; set; }
    }
}
            