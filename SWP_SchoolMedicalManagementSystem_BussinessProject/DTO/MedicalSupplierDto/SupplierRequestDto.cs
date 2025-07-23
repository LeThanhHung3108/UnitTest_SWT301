using SchoolMedicalManagementSystem.Enum;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalSupplierDto
{
    public class SupplierRequestDto
    {
        public string? SupplyName { get; set; }
        public SupplyType SupplyType { get; set; }
        public string? Unit { get; set; }
        public int Quantity { get; set; }
        public string? Supplier { get; set; }
        public List<string>? Image { get; set; }
    }
}
