namespace SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.NotifyDto
{
    public class NotificationRequest
    {
        public Guid CampaignId { get; set; }
        public string? Title { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
