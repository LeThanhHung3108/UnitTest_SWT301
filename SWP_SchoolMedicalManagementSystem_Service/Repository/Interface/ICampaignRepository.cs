using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SchoolMedicalManagementSystem.Enum;

namespace SWP_SchoolMedicalManagementSystem_Service.Repository.Interface
{
    public interface ICampaignRepository
    {
        Task<List<Campaign>> GetAllCampaignsAsync();
        Task<Campaign?> GetCampaignByIdAsync(Guid campaignId);
        Task<List<Campaign>> GetCampaignsByStatusAsync(CampaignStatus campaignStatus);
        Task<List<Campaign>> GetCampaignsByTypeAsync(CampaignType campaignType);
        Task CreateCampaignAsync(Campaign campaign);
        Task UpdateCampaignAsync(Campaign campaign);
        Task DeleteCampaignAsync(Guid campaignId);
    }
}
