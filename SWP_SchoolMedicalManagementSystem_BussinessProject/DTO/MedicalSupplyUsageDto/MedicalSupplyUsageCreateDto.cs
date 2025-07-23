namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalSupplyUsageDto
{
    public class MedicalSupplyUsageCreateDto
    {
        public Guid MedicalSupplierId { get; set; }
        public int QuantityUsed { get; set; }
        public DateTime UsageDate { get; set; }
        public string? Notes { get; set; }
    }
}
