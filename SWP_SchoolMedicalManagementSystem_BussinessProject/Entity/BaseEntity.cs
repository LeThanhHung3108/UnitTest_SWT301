namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Entity
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
