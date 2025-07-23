namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Entity
{
    public class Schedule : BaseEntity
    {
        public Guid CampaignId { get; set; }
        public Campaign? Campaign { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string? Location { get; set; }
        public string? Notes { get; set; }
        public ICollection<ScheduleDetail>? ScheduleDetails { get; set; }
    }
}
