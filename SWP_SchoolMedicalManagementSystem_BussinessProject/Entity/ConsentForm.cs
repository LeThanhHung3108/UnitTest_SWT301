namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Entity
{
    public class ConsentForm : BaseEntity
    {
        public Guid CampaignId { get; set; }
        public Campaign? Campaign { get; set; }
        public Guid StudentId { get; set; }
        public Student? Student { get; set; }
        public bool IsApproved { get; set; } = false;
        public DateTime ConsentDate { get; set; }
        public string? ReasonForDecline { get; set; }
    }
}
