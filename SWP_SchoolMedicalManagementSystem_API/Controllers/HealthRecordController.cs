using Microsoft.AspNetCore.Mvc;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.HealthRecordDto;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;

namespace SWP_SchoolMedicalManagementSystem_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthRecordController : ControllerBase
    {
        private readonly IHealthRecordService _healthRecordService;

        public HealthRecordController(IHealthRecordService healthRecordService)
        {
            _healthRecordService = healthRecordService;
        }

        //1. Get All Health Records
        [HttpGet("get-all-health-records")]
        public async Task<IActionResult> GetAllHealthRecord()
        {
            var healthRecords = await _healthRecordService.GetAllHealthRecordAsync();
            return Ok(healthRecords);
        }


        //2. Get Health Record by ID
        [HttpGet("get-health-record-by-id/{healthRecordId}")]
        public async Task<IActionResult> GetHealthRecordById(Guid healthRecordId)
        {
            var healthRecord = await _healthRecordService.GetHealthRecordByIdAsync(healthRecordId);
            if (healthRecord == null)
                return NotFound();
            return Ok(healthRecord);
        }

        //3. Get Health Record by Student ID
        [HttpGet("get-health-record-by-student-id/{studentId}")]
        public async Task<IActionResult> GetByStudentId(Guid studentId)
        {
            var healthRecord = await _healthRecordService.GetHealthRecordByStudentIdAsync(studentId);
            if (healthRecord == null)
                return NotFound();
            return Ok(healthRecord);
            
        }

        //4. Create Health Record
        [HttpPost("create-health-record")]
        public async Task<IActionResult> CreateHealthRecord([FromBody] HealthRecordRequest request)
        {
            await _healthRecordService.CreateHealthRecordAsync(request);
            return Ok("HealthRecord created successfully.");
        }

        //5. Update Health Record
        [HttpPut("update-health-record/{healthRecordId}")]
        public async Task<IActionResult> UpdateHealthRecord(Guid healthRecordId, [FromBody] HealthRecordRequest request)
        {
                if (request == null)
                {
                    return BadRequest("Invalid request data");
                }

                await _healthRecordService.UpdateHealthRecordAsync(healthRecordId, request);
                return Ok("HealthRecord updated successfully.");
        }

        //6. Delete Health Record
        [HttpDelete("delete-health-record/{healthRecordId}")]
        public async Task<IActionResult> DeleteHealthRecord(Guid healthRecordId)
        {          
           await _healthRecordService.DeleteHealthRecordAsync(healthRecordId);
           return Ok("HealthRecord deleted successfuly.");
        }
    }
}
