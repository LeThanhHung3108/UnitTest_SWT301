using Microsoft.AspNetCore.Mvc;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalConsultationDto;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;

namespace SWP_SchoolMedicalManagementSystem_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalConsultationController : ControllerBase
    {
        private readonly IMedicalConsultationService _medicalConsultationService;

        public MedicalConsultationController(IMedicalConsultationService medicalConsultationService)
        {
            _medicalConsultationService = medicalConsultationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var consultations = await _medicalConsultationService.GetAllMedicalConsultationsAsync();
            return Ok(consultations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var consultation = await _medicalConsultationService.GetMedicalConsultationByIdAsync(id);
            if (consultation == null)
            {
                return NotFound($"Medical consultation with ID {id} not found.");
            }
            return Ok(consultation);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MedicalConsultationCreateRequestDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }
            await _medicalConsultationService.CreateMedicalConsultationAsync(request);
            return Ok("Medical consultation created successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MedicalConsultationUpdateRequesteDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }
            await _medicalConsultationService.UpdateMedicalConsultationAsync(id, request);
            return Ok("Medical consultation updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _medicalConsultationService.DeleteMedicalConsultationAsync(id);
                return Ok("Medical consultation deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
