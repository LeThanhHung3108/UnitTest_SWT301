using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;

namespace SWP_SchoolMedicalManagementSystem_Service.Repository.Interface
{
    public interface IMedicalSupplierRepository
    {
        Task CreateSupplierAsync(MedicalSupply supplier);
        Task UpdateSupplierAsync(MedicalSupply supplier);
        Task DeleteSupplierAsync(Guid id);
        Task<MedicalSupply?> GetSupplierByIdAsync(Guid id);
        Task<List<MedicalSupply>> GetAllSuppliersAsync();
    }
}
