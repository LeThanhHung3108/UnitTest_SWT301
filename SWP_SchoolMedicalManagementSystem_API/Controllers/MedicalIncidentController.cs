 using Microsoft.AspNetCore.Mvc;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalIncidentDto;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;

namespace SWP_SchoolMedicalManagementSystem_API.Controllers
{
    [ApiController]
    [Route("api/medical-incident")]
    public class MedicalIncidentController : ControllerBase
    {
        private readonly IMedicalIncidentService _medicalIncidentService;

        public MedicalIncidentController(IMedicalIncidentService medicalIncidentService)
        {
            _medicalIncidentService = medicalIncidentService ?? throw new ArgumentNullException(nameof(medicalIncidentService));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllIncidents()
        {
            var incidents = await _medicalIncidentService.GetAllIncidentsAsync();
            return Ok(incidents);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIncidentById(Guid id)
        {
            var incident = await _medicalIncidentService.GetIncidentByIdAsync(id);
            if (incident == null)
            {
                return NotFound($"Incident with ID {id} not found.");
            }
            return Ok(incident);
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetIncidentsByStudentId(Guid studentId)
        {
            var incidents = await _medicalIncidentService.GetIncidentsByStudentIdAsync(studentId);
            if (incidents == null || !incidents.Any())
            {
                return NotFound($"No incidents found for student ID {studentId}.");
            }
            return Ok(incidents);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateIncident([FromBody] IncidentCreateRequestDto incident)
        {
            await _medicalIncidentService.CreateIncidentAsync(incident);
            return Ok("Incident created successfully.");
        }

        [HttpDelete("delete/{incidentId}")]
        public async Task<IActionResult> DeleteIncident([FromBody] Guid id)
        {
            await _medicalIncidentService.DeleteIncidentAsync(id);
            return Ok("Incident deleted successfully.");
        }

        [HttpPut("update/{incidentId}")]
        public async Task<IActionResult> UpdateIncident(Guid incidentId, [FromBody] IncidentUpdateRequestDto incident)
        {
            await _medicalIncidentService.UpdateIncidentAsync(incidentId, incident);
            return Ok("Incident updated successfully.");
        }
    }
}
