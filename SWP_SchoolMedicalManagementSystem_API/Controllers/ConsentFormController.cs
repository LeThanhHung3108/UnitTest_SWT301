using Microsoft.AspNetCore.Mvc;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccFormDto;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;

namespace SWP_SchoolMedicalManagementSystem_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsentFormController : ControllerBase
    {
        private readonly IConsentFormService _consentFormService;

        public ConsentFormController(IConsentFormService consentFormService)
        {
            _consentFormService = consentFormService;
        }

        [HttpGet("get-all-consent-forms")]
        public async Task<ActionResult<List<ConsentFormResponse>>> GetAllConsentForms()
        {
            var consentForms = await _consentFormService.GetAllConsentFormsAsync();
            return Ok(consentForms);
        }

        [HttpGet("get-consent-form-by-id/{consentFormId}")]
        public async Task<ActionResult<ConsentFormResponse>> GetConsentFormById(Guid consentFormId)
        {
             var consentForm = await _consentFormService.GetConsentFormByIdAsync(consentFormId);
             if (consentForm == null)
                 return NotFound();
             return Ok(consentForm);
        }

        [HttpGet("get-consent-forms-by-campaign-id/{campaignId}")]
        public async Task<ActionResult<List<ConsentFormResponse>>> GetConsentFormsByCampaign(Guid campaignId)
        {
            var consentForms = await _consentFormService.GetConsentFormsByCampaignIdAsync(campaignId);
            return Ok(consentForms);
        }

        [HttpGet("get-consent-forms-by-student-id/{studentId}")]
        public async Task<ActionResult<List<ConsentFormResponse>>> GetConsentFormsByStudent(Guid studentId)
        {
            var consentForms = await _consentFormService.GetConsentFormsByStudentIdAsync(studentId);
            return Ok(consentForms);
        }

        [HttpPost("create-consent-form")]
        public async Task<ActionResult> CreateConsentForm([FromBody] ConsentFormRequest request)
        {
             await _consentFormService.CreateConsentFormAsync(request);
             return Ok("Consent form created successfully");

        }

        [HttpPut("update-consent-form/{consentFormId}")]
        public async Task<ActionResult> UpdateConsentForm(Guid consentFormId, [FromBody] ConsentFormRequest request)
        {
             await _consentFormService.UpdateConsentFormAsync(consentFormId, request);
             return Ok("Consent form updated successfully");
        }

        [HttpDelete("delete-consent-form/{consentFormId}")]
        public async Task<ActionResult> DeleteConsentForm(Guid consentFormId)
        {
             await _consentFormService.DeleteConsentFormAsync(consentFormId);
             return Ok("Consent form deleted successfully");

        }
    }
}
