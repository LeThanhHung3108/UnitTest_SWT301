using AutoMapper;
using Microsoft.AspNetCore.Http;
using SchoolMedicalManagementSystem.Enum;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccCampaignDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Repository.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;

namespace SWP_SchoolMedicalManagementSystem_Service.Service
{
    public class CampaignService : ICampaignService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICampaignRepository _campaignRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;

        public CampaignService(IHttpContextAccessor httpContextAccessor, ICampaignRepository campaignRepository,
            IScheduleRepository scheduleRepository, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _campaignRepository = campaignRepository;
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;

        }

        //1. Get all campaigns
        public async Task<List<CampaignResponse>> GetAllCampaignsAsync()
        {
            var campaigns = await _campaignRepository.GetAllCampaignsAsync();
            if (campaigns == null || !campaigns.Any())
                throw new KeyNotFoundException("No campaigns found.");
            return _mapper.Map<List<CampaignResponse>>(campaigns);
        }

        //2. Get campaign by ID
        public async Task<CampaignResponse?> GetCampaignByIdAsync(Guid campaignId)
        {
            var campaign = await _campaignRepository.GetCampaignByIdAsync(campaignId);
            if (campaign == null)
                throw new KeyNotFoundException($"Campaign with ID {campaignId} not found.");
            return _mapper.Map<CampaignResponse>(campaign);
        }

        //3. Get campaigns by status
        public async Task<List<CampaignResponse>> GetCampaignsByStatusAsync(CampaignStatus campaignStatus)
        {
            var campaigns = await _campaignRepository.GetCampaignsByStatusAsync(campaignStatus);
            if (campaigns == null || !campaigns.Any())
                throw new KeyNotFoundException($"No campaigns found with status {campaignStatus}.");
            return _mapper.Map<List<CampaignResponse>>(campaigns);
        }

        //4. Get campaigns by type
        public async Task<List<CampaignResponse>> GetCampaignsByTypeAsync(CampaignType campaignType)
        {
            var campaigns = await _campaignRepository.GetCampaignsByTypeAsync(campaignType);
            if (campaigns == null || !campaigns.Any())
                throw new KeyNotFoundException($"No campaigns found with status {campaignType}.");
            return _mapper.Map<List<CampaignResponse>>(campaigns);
        }

        //4. Create a new campaign
        public async Task CreateCampaignAsync(CampaignRequest campaign)
        {
            try
            {
                var newCampaign = _mapper.Map<Campaign>(campaign);
                newCampaign.CreatedBy = GetCurrentUsername();
                newCampaign.CreateAt = DateTime.UtcNow;
                newCampaign.Schedules = campaign.Schedules?.Select(s => new Schedule
                {
                    Id = Guid.NewGuid(),
                    CampaignId = newCampaign.Id,
                    Campaign = newCampaign,
                    ScheduledDate = s.ScheduledDate,
                    Location = s.Location,
                    Notes = s.Notes,
                    CreatedBy = GetCurrentUsername(),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                }).ToList() ?? new List<Schedule>();

                await _campaignRepository.CreateCampaignAsync(newCampaign);

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while creating the campaign.", ex);
            }
        }

        //5. Update an existing campaign
        public async Task UpdateCampaignAsync(Guid campaignId, CampaignRequest campaign)
        {
            var existingCampaign = await _campaignRepository.GetCampaignByIdAsync(campaignId);
            if (existingCampaign == null)
                throw new KeyNotFoundException($"Campaign with ID {campaignId} not found.");

            existingCampaign.UpdatedBy = GetCurrentUsername();
            existingCampaign.UpdateAt = DateTime.UtcNow;
            _mapper.Map(campaign, existingCampaign);
            await _campaignRepository.UpdateCampaignAsync(existingCampaign);
        }

        //6. Delete a campaign
        public async Task DeleteCampaignAsync(Guid campaignId)
        {
            var existingCampaign = await _campaignRepository.GetCampaignByIdAsync(campaignId);
            if (existingCampaign == null)
                throw new KeyNotFoundException($"Campaign with ID {campaignId} not found.");
            foreach (var schedule in existingCampaign.Schedules ?? new List<Schedule>())
            {
                await _scheduleRepository.DeleteScheduleAsync(schedule.Id);
            }

            await _campaignRepository.DeleteCampaignAsync(campaignId);
        }

        //7. Get current username from HTTP context
        private string GetCurrentUsername()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirst("username")?.Value ?? "Unknown User";
        }
    }
}