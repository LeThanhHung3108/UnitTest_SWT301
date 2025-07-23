using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolMedicalManagementSystem.Enum;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.MedicationRequestsDto;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;

namespace SWP_SchoolMedicalManagementSystem_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MedicationRequestController : ControllerBase
    {
        private readonly IMedicalRequestService _medicalRequestService;

        public MedicationRequestController(IMedicalRequestService medicalRequestService)
        {
            _medicalRequestService = medicalRequestService;
        }

        //1. Get all medication requests
        [HttpGet("get-all-medication-requests")]
        public async Task<IActionResult> GetAllMedicationRequests()
        {
            var medicationRequests = await _medicalRequestService.GetAllMedicationRequests();
            return Ok(medicationRequests);
        }

        //2. Get medication request by ID
        [HttpGet("get-medication-request-by-id/{medicalReqId}")]
        public async Task<IActionResult> GetMedicationRequestById(Guid medicalReqId)
        {
            var medicationRequest = await _medicalRequestService.GetMedicationRequestById(medicalReqId);
            if (medicationRequest == null)
            {
                return NotFound($"Medication request with ID {medicalReqId} not found.");
            }
            return Ok(medicationRequest);
        }

        //3. Get medication requests by Student ID
        [HttpGet("get-medication-requests-by-student-id/{studentId}")]
        public async Task<IActionResult> GetMedicationRequestsByStudentId(Guid studentId)
        {
            var medicationRequests = await _medicalRequestService.GetMedicationRequestsByStudentId(studentId);
            if (medicationRequests == null || !medicationRequests.Any())
            {
                return NotFound($"No medication requests found for student ID {studentId}.");
            }
            return Ok(medicationRequests);
        }

        //4. Get medication requests by status
        [HttpGet("get-medication-requests-by-status/{status}")]
        public async Task<IActionResult> GetMedicationRequestsByStatus(RequestStatus status)
        {
            var medicationRequests = await _medicalRequestService.GetMedicationRequestsByStatus(status);
            if (medicationRequests == null || !medicationRequests.Any())
            {
                return NotFound($"No medication requests found with status {status}.");
            }
            return Ok(medicationRequests);
        }

        //5. Create a new medication request
        [HttpPost("create-medication-request")]
        public async Task<IActionResult> CreateMedicationRequest([FromBody] MedicationReqRequest request)
        {
            await _medicalRequestService.CreateMedicationRequest(request);
            return Ok("Medication request created successfully.");
        }

        //6. Update an existing medication request
        [HttpPut("update-medication-request/{medicalReqId}")]
        public async Task<IActionResult> UpdateMedicationRequest(Guid medicalReqId, [FromBody] MedicationReqRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid update request.");
            }

            await _medicalRequestService.UpdateMedicationRequest(medicalReqId, request);
            return Ok("Medication request updated successfully.");
        }

        //7. Delete a medication request
        [HttpDelete("delete-medication-request/{medicalReqId}")]
        public async Task<IActionResult> DeleteMedicationRequest(Guid medicalReqId)
        {
            await _medicalRequestService.DeleteMedicationRequest(medicalReqId);
            return Ok("Medication request deleted successfully.");
        }

        //8. Approve a medication request
        [HttpPost("approve-medication-request/{medicalReqId}")]
        public async Task<IActionResult> ApproveMedicationRequest(Guid medicalReqId)
        {
            await _medicalRequestService.AccecptMedicationRequest(medicalReqId);
            return Ok("Medication request approved successfully.");
        }

        //9. Reject a medication request
        [HttpPost("reject-medication-request/{medicalReqId}")]
        public async Task<IActionResult> RejectMedicationRequest(Guid medicalReqId)
        {
            await _medicalRequestService.RejectMedicationRequest(medicalReqId);
            return Ok("Medication request rejected successfully.");
        }

    }
}
