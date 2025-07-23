using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalSupplierDto;

namespace SWP_SchoolMedicalManagementSystem_Service.IService
{
    public interface IMedicalSupplierService
    {
        Task CreateSupplierAsync(SupplierRequestDto supplier);
        Task UpdateSupplierAsync(Guid id, SupplierRequestDto supplier);
        Task DeleteSupplierAsync(Guid id);
        Task<SupplierResponseDto> GetSupplierByIdAsync(Guid id);
        Task<List<SupplierResponseDto>> GetAllSuppliersAsync();
    }
}
