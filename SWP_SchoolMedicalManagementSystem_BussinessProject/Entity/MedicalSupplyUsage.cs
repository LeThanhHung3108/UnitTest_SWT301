namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Entity
{
    public class MedicalSupplyUsage
    {
        public Guid? IncidentId { get; set; }
        public MedicalIncident? Incident { get; set; }
        public Guid? SupplyId { get; set; }
        public MedicalSupply? MedicalSupply {  get; set; }
        public int QuantityUsed { get; set; }
        public DateTime UsageDate { get; set; }
        public string? Notes { get; set; }   
    }
}
