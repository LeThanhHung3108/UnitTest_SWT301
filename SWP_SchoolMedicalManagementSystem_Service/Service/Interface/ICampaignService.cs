using SchoolMedicalManagementSystem.Enum;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccCampaignDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_Service.Service.Interface
{
    public interface ICampaignService
    {
        Task<List<CampaignResponse>> GetAllCampaignsAsync();
        Task<CampaignResponse?> GetCampaignByIdAsync(Guid campaignId);
        Task<List<CampaignResponse>> GetCampaignsByStatusAsync(CampaignStatus campaignStatus);
        Task<List<CampaignResponse>> GetCampaignsByTypeAsync(CampaignType campaignType);
        Task CreateCampaignAsync(CampaignRequest campaign);
        Task UpdateCampaignAsync(Guid campaignId, CampaignRequest campaign);
        Task DeleteCampaignAsync(Guid campaignId);
    }
}
