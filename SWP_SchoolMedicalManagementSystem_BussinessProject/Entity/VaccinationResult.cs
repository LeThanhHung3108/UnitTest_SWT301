namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Entity
{
    public  class VaccinationResult : BaseEntity
    {
        public Guid? ScheduleDetailId { get; set; }
        public string? DosageGiven {  get; set; }
        public string? SideEffects {  get; set; }
        public string? Notes { get; set; }
        public ScheduleDetail? ScheduleDetail { get; set; }
        public MedicalConsultation? MedicalConsultation { get; set; }
    }
}
