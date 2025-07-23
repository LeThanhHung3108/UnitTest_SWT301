using Microsoft.AspNetCore.Mvc;
using SchoolMedicalManagementSystem.Enum;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccCampaignDto;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;

namespace SWP_SchoolMedicalManagementSystem_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignService _campaignService;

        public CampaignController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        //1. Get all campaigns
        [HttpGet("get-all-campaigns")]
        public async Task<IActionResult> GetAllCampaigns()
        {
            var campaigns = await _campaignService.GetAllCampaignsAsync();
            return Ok(campaigns);
        }

        //2. Get campaign by ID
        [HttpGet("get-campaign-by-id/{campaignId}")]
        public async Task<IActionResult> GetVaccCampaignById(Guid campaignId)
        {
            var campaign = await _campaignService.GetCampaignByIdAsync(campaignId);
            if (campaign == null)
            {
                return NotFound($"Campaign with ID {campaignId} not found.");
            }
            return Ok(campaign);
        }

        //3. Get campaigns by status
        [HttpGet("get-campaigns-by-status/{status}")]
        public async Task<IActionResult> GetCampaignsByStatus(CampaignStatus status)
        {
            var campaign = await _campaignService.GetCampaignsByStatusAsync(status);
            if (campaign == null || !campaign.Any())
            {
                return NotFound($"No campaigns found with status {status}.");
            }
            return Ok(campaign);
        }

        //3. Get campaigns by type
        [HttpGet("get-campaigns-by-type/{type}")]
        public async Task<IActionResult> GetCampaignsByType(CampaignType type)
        {
            var campaign = await _campaignService.GetCampaignsByTypeAsync(type);
            if (campaign == null || !campaign.Any())
            {
                return NotFound($"No campaigns found with status {type}.");
            }
            return Ok(campaign);
        }

        //4. Create a new campaign
        [HttpPost("create-campaign")]
        public async Task<IActionResult> CreateCampaign([FromBody] CampaignRequest request)
        {
            if (request == null)
            {
                return BadRequest("Campaign data is required.");
            }

            await _campaignService.CreateCampaignAsync(request);
            return Ok("Campaign created successfully.");
        }

        //5. Update an existing campaign
        [HttpPut("update-campaign/{campaignId}")]
        public async Task<IActionResult> UpdateCampaign(Guid campaignId, [FromBody] CampaignRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid update request.");
            }

            await _campaignService.UpdateCampaignAsync(campaignId, request);
            return Ok("Campaign updated successfully.");
        }

        //6. Delete a campaign
        [HttpDelete("delete-campaign/{campaignId}")]
        public async Task<IActionResult> DeleteCampaign(Guid campaignId)
        {
            await _campaignService.DeleteCampaignAsync(campaignId);
            return Ok("Campaign deleted successfully.");
        }
    }
}
