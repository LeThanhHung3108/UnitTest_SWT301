using Microsoft.AspNetCore.Mvc;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.ScheduleDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccScheduleDto;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;

namespace SWP_SchoolMedicalManagementSystem_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        //1. Get all schedules
        [HttpGet("get-all-schedules")]
        public async Task<IActionResult> GetAllSchedules()
        {
            var schedules = await _scheduleService.GetAllSchedulesAsync();
            return Ok(schedules);
        }

        //2. Get schedule by ID
        [HttpGet("get-schedule-by-id/{scheduleId}")]
        public async Task<IActionResult> GetScheduleById(Guid scheduleId)
        {
            var schedule = await _scheduleService.GetScheduleByIdAsync(scheduleId);
            if (schedule == null)
                return NotFound();
            return Ok(schedule);
        }

        //3. Get schedules by campaign ID
        [HttpGet("get-schedules-by-campaign-id/{campaignId}")]
        public async Task<IActionResult> GetSchedulesByCampaignId(Guid campaignId)
        {
            var schedules = await _scheduleService.GetSchedulesByCampaignIdAsync(campaignId);
            return Ok(schedules);
        }

        //4. Create schedule
        [HttpPost("create-schedule")]
        public async Task<IActionResult> CreateSchedule([FromBody] ScheduleCreateDto request)
        {
            await _scheduleService.CreateScheduleAsync(request);
            return Ok("Schedule created successfully.");
        }

        // Assign students to a schedule
        [HttpPost("assign-students-to-schedule")]
        public async Task<IActionResult> AssignStudentsToSchedule([FromBody] AssignStudentToScheduleDto request)
        {
            if (request == null || request.ScheduleId == Guid.Empty || request.StudentIds == null || !request.StudentIds.Any())
            {
                return BadRequest("Invalid request data.");
            }
            await _scheduleService.AssignStudentToScheduleAsync(request);
            return Ok("Students assigned to schedule successfully.");
        }
        //5. Update schedule
        [HttpPut("update-schedule/{scheduleId}")]
        public async Task<IActionResult> UpdateSchedule(Guid scheduleId, [FromBody] ScheduleBaseRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid update request.");
            }
            await _scheduleService.UpdateScheduleAsync(scheduleId, request);
            return Ok("Schedule updated successfully.");
        }

        //6. Delete schedule
        [HttpDelete("delete-schedule/{scheduleId}")]
        public async Task<IActionResult> DeleteSchedule(Guid scheduleId)
        {
            await _scheduleService.DeleteScheduleAsync(scheduleId);
            return Ok("Schedule deleted successfully.");
        }
    }
}
