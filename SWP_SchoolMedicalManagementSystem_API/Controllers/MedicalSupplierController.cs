using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalSupplierDto;
using SWP_SchoolMedicalManagementSystem_Service.IService;

namespace SWP_SchoolMedicalManagementSystem_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalSupplierController : ControllerBase
    {
        private readonly IMedicalSupplierService _supplierService;

        public MedicalSupplierController(IMedicalSupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet("get-all-suppliers")]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            return Ok(suppliers);
        }

        [HttpGet("get-supplier-by-id/{id}")]
        public async Task<IActionResult> GetSupplierById(Guid id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            if (supplier == null)
                return NotFound();
            return Ok(supplier);
        }

        [HttpPost("create-supplier")]
        public async Task<IActionResult> CreateSupplier([FromBody] SupplierRequestDto request)
        {
            if (request == null)
            {
                return BadRequest("Supplier data is required.");
            }

            await _supplierService.CreateSupplierAsync(request);
            return Ok("Supplier created successfully.");
        }

        [HttpPut("update-supplier/{id}")]
        public async Task<IActionResult> UpdateSupplier(Guid id, [FromBody] SupplierRequestDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid update request.");
            }

            await _supplierService.UpdateSupplierAsync(id, request);
            return Ok("Supplier updated successfully.");
        }

        [HttpDelete("delete-supplier/{id}")]
        public async Task<IActionResult> DeleteSupplier(Guid id)
        {
            await _supplierService.DeleteSupplierAsync(id);
            return Ok("Supplier deleted successfully.");
        }
    }
}
