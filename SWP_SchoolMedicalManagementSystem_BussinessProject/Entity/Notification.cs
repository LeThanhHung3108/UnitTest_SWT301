namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Entity
{
    public class Notification : BaseEntity
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? ReturnUrl { get; set; }    
        public ICollection<User>? Users { get; set; }
    }
}
