using AutoMapper;
using Microsoft.AspNetCore.Http;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalSupplierDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Service.IService;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;

namespace SWP_SchoolMedicalManagementSystem_Service.Service
{
    public class MedicalSupplierService : IMedicalSupplierService
    {
        private readonly IMedicalSupplierRepository _supplyRepo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MedicalSupplierService(IMedicalSupplierRepository supplyRepo, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _supplyRepo = supplyRepo ?? throw new ArgumentNullException(nameof(supplyRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task CreateSupplierAsync(SupplierRequestDto supplier)
        {
            try
            {
                var newSupplier = _mapper.Map<MedicalSupply>(supplier);
                newSupplier.CreatedBy = GetCurrentUsername();
                newSupplier.CreateAt = DateTime.UtcNow;
                await _supplyRepo.CreateSupplierAsync(newSupplier);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating supplier: {ex.Message}", ex);
            }
        }

        public async Task DeleteSupplierAsync(Guid id)
        {
            try
            {
                await _supplyRepo.DeleteSupplierAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting supplier with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<List<SupplierResponseDto>> GetAllSuppliersAsync()
        {
            try
            {
                var suppliers = await _supplyRepo.GetAllSuppliersAsync();
                return suppliers.Select(s => _mapper.Map<SupplierResponseDto>(s)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all suppliers: {ex.Message}", ex);
            }
        }

        public async Task<SupplierResponseDto> GetSupplierByIdAsync(Guid id)
        {
            try
            {
                var supplier = await _supplyRepo.GetSupplierByIdAsync(id);
                return _mapper.Map<SupplierResponseDto>(supplier) ?? throw new KeyNotFoundException($"Supplier with ID {id} not found.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving supplier with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task UpdateSupplierAsync(Guid id, SupplierRequestDto supplier)
        {
            try
            {
                var oldSupplier = await _supplyRepo.GetSupplierByIdAsync(id);
                if(oldSupplier == null)
                {
                    throw new KeyNotFoundException($"Supplier with ID {id} not found.");
                }
                _mapper.Map(supplier,oldSupplier);
                await _supplyRepo.UpdateSupplierAsync(oldSupplier);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error update supplier with ID {id}: {ex.Message}", ex);
            }
        }

        private string GetCurrentUsername()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirst("username")?.Value ?? "Unknown User";
        }
    }
}
