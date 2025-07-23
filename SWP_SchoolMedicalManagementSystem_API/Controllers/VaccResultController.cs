using Microsoft.AspNetCore.Mvc;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccResultDto;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;


namespace SWP_SchoolMedicalManagementSystem_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccResultController : ControllerBase
    {
        private readonly IVaccResultService _vaccResultService;

        public VaccResultController(IVaccResultService vaccResultService)
        {
            _vaccResultService = vaccResultService;
        }

        //1. Get all vaccination results
        [HttpGet("get-all-vacc-results")]
        public async Task<IActionResult> GetAllVaccResults()
        {
            var vaccResults = await _vaccResultService.GetAllVaccResultsAsync();
            return Ok(vaccResults);
        }

        //2. Get vaccination result by ID
        [HttpGet("get-vacc-result-by-id/{vaccResultId}")]
        public async Task<IActionResult> GetVaccResultById(Guid vaccResultId)
        {
            var vaccResults = await _vaccResultService.GetVaccResultByIdAsync(vaccResultId);
            if (vaccResults == null)
                return NotFound();
            return Ok(vaccResults);
        }

        //3.Create Vaccination Result
        [HttpPost("create-vacc-result")]
        public async Task<IActionResult> CreateVaccResult([FromBody] VaccResultRequest request)
        {
            await _vaccResultService.CreateVaccResultAsync(request);
            return Ok("Vaccination result created successfully.");
        }

        //4. Update Vaccination Result
        [HttpPut("update-vacc-result/{vaccResultId}")]
        public async Task<IActionResult> UpdateVaccResult(Guid vaccResultId, [FromBody] VaccResultRequest request)
        {
            await _vaccResultService.UpdateVaccResultAsync(vaccResultId, request);
            return Ok("Vaccination result updated successfully.");
        }

        //5. Delete Vaccination Result
        [HttpDelete("delete-vacc-result/{vaccResultId}")]
        public async Task<IActionResult> DeleteVaccResult(Guid vaccResultId)
        {
            await _vaccResultService.DeleteVaccResultAsync(vaccResultId);
            return Ok("Vaccination result deleted successfully.");
        }
    }
}
