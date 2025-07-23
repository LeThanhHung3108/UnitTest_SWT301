using Microsoft.AspNetCore.Mvc;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.ScheduleDetailDto;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleDetailController : ControllerBase
    {
        private readonly IScheduleDetailService _scheduleDetailService;

        public ScheduleDetailController(IScheduleDetailService scheduleDetailService)
        {
            _scheduleDetailService = scheduleDetailService;
        }

        //1. Get all schedule details
        [HttpGet("get-all-schedule-detail")]
        public async Task<IActionResult> GetAllScheduleDetails()
        {
            var scheduleDetails = await _scheduleDetailService.GetAllScheduleDetailsAsync();
            return Ok(scheduleDetails);
        }

        //2. Get schedule detail by ID
        [HttpGet("get-schedule-detail-by-id/{scheduleDetailId}")]
        public async Task<IActionResult> GetScheduleDetailById(Guid scheduleDetailId)
        {
            var scheduleDetail = await _scheduleDetailService.GetScheduleDetailByIdAsync(scheduleDetailId);
            return Ok(scheduleDetail);
        }

        //3. Get schedule details by Schedule ID
        [HttpGet("get-schedule-details-by-schedule-id/{scheduleId}")]
        public async Task<IActionResult> GetScheduleDetailByScheduleId(Guid scheduleId)
        {
            var scheduleDetails = await _scheduleDetailService.GetScheduleDetailsByScheduleIdAsync(scheduleId);
            return Ok(scheduleDetails);
        }

        //4. Get schedule details by Student ID
        [HttpGet("get-schedule-details-by-student-id/{studentId}")]
        public async Task<IActionResult> GetScheduleDetailByStudentId(Guid studentId)
        {
            var scheduleDetails = await _scheduleDetailService.GetScheduleDetailsByStudentIdAsync(studentId);
            return Ok(scheduleDetails);
        }

        //5. Create schedule detail
        [HttpPost("create-schedule-detail")]
        public async Task<IActionResult> CreateScheduleDetail([FromBody] ScheduleDetailRequest request)
        {
            await _scheduleDetailService.CreateScheduleDetailAsync(request);
            return Ok("ScheduleDetail created successfully.");
        }

        //6. Update schedule detail
        [HttpPut("update-schedule-detail/{scheduleDetailId}")]
        public async Task<IActionResult> UpdateScheduleDetail(Guid scheduleDetailId, [FromBody] ScheduleDetailRequest request)
        {
            await _scheduleDetailService.UpdateScheduleDetailAsync(scheduleDetailId, request);
            return Ok("ScheduleDetail updated successfully.");
        }

        //7. Delete schedule detail
        [HttpDelete("delete-schedule-detail/{scheduleDetailId}")]
        public async Task<IActionResult> DeleteScheduleDetail(Guid scheduleDetailId)
        {
            await _scheduleDetailService.DeleteScheduleDetailAsync(scheduleDetailId);
            return Ok("ScheduleDetail deleted successfully.");
        }
    }
}
