using Microsoft.AspNetCore.Mvc;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.HealthCheckupResultDto;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;

namespace SWP_SchoolMedicalManagementSystem_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthCheckupResultController : ControllerBase
    {
        private readonly IHealthCheckupResultService _healthresultService;

        public HealthCheckupResultController(IHealthCheckupResultService healthresultService)
        {
            _healthresultService = healthresultService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var results = await _healthresultService.GetAll();
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _healthresultService.GetById(id);
            if (result == null)
            {
                return NotFound($"Health checkup result with ID {id} not found.");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HealthCheckupCreateRequestDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }
            await _healthresultService.CreateHealthCheckupResult(request);
            return Ok("Health checkup result created successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] HealthCheckupUpdateRequestDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }
            await _healthresultService.UpdateHealthCheckupResult(id, request);
            return Ok("Health checkup result updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _healthresultService.DeleteHealthCheckupResult(id);
                return Ok("Health checkup result deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
