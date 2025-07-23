using Microsoft.AspNetCore.Mvc;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalDiaryDto;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;

namespace SWP_SchoolMedicalManagementSystem_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicaDiaryController : ControllerBase
    {
        private readonly IMedicalDiaryService _medicalDiaryService;

        public MedicaDiaryController(IMedicalDiaryService medicalDiaryService)
        {
            _medicalDiaryService = medicalDiaryService ?? throw new ArgumentNullException(nameof(medicalDiaryService));
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllMedicineDiary()
        {
            var diaries = await _medicalDiaryService.GetAllMedicineDiary();
            return Ok(diaries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicineDiaryById(Guid id)
        {
            var diary = await _medicalDiaryService.GetMedicineDiaryById(id);
            if (diary == null)
                return NotFound();
            return Ok(diary);
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetMedicineDiaryByStudentId(Guid studentId)
        {
            var diaries = await _medicalDiaryService.GetMedicineDiaryByStudentId(studentId);
            return Ok(diaries);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMedicineDiary([FromBody] MedicalDiaryRequestDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }
            await _medicalDiaryService.CreateMedicineDiary(request);
            return Ok("Medical diary created successfully.");
        }

        [HttpPut("{diaryId}")]
        public async Task<IActionResult> UpdateMedicineDiary(Guid diaryId, [FromBody] MedicalDiaryRequestDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data.");
            }
            await _medicalDiaryService.UpdateMedicineDiary(diaryId, request);
            return Ok("Medical diary updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicineDiary(Guid id)
        {
            await _medicalDiaryService.DeleteMedicineDiary(id);
            return Ok("Medical diary deleted successfully.");

        }
    }
}
