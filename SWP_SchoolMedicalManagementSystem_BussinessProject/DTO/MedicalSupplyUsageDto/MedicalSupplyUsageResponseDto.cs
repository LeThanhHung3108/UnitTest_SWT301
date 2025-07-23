using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalSupplierDto;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalSupplyUsageDto
{
    public class MedicalSupplyUsageResponseDto
    {
        public SupplierResponseDto? MedicalSupply { get; set; }
        public int QuantityUsed { get; set; }
        public DateTime UsageDate { get; set; }
        public string? Notes { get; set; }
    }
}
