using Microsoft.EntityFrameworkCore;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Context;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using SchoolMedicalManagementSystem.Enum;

namespace SWP_SchoolMedicalManagementSystem_Service.Repository
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly ApplicationDBContext _context;

        public CampaignRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        //1. Get all campaigns
        public async Task<List<Campaign>> GetAllCampaignsAsync()
        {
            return await _context.Campaigns
                .Include(v => v.Schedules)
                .ToListAsync();
        }

        //2. Get campaign by ID
        public async Task<Campaign?> GetCampaignByIdAsync(Guid campaignId)
        {
            return await _context.Campaigns
                .Include(v => v.Schedules)
                .ThenInclude(s => s.ScheduleDetails)
                .ThenInclude(s => s.Student)
                .ThenInclude(s => s.Parent)
                .FirstOrDefaultAsync(v => v.Id == campaignId);
        }

        //3. Get campaigns by status
        public async Task<List<Campaign>> GetCampaignsByStatusAsync(CampaignStatus campaignStatus)
        {
            return await _context.Campaigns
                .Include(v => v.Schedules)
                .Where(v => v.Status == campaignStatus)
                .AsNoTracking()
                .ToListAsync();
        }

        //4. Get campaigns by type
        public async Task<List<Campaign>> GetCampaignsByTypeAsync(CampaignType campaignType) { 
            return await _context.Campaigns
                .Include(v => v.Schedules)
                .Where(v => v.Type == campaignType)
                .AsNoTracking()
                .ToListAsync();
        }

        //5. Create campaign
        public async Task CreateCampaignAsync(Campaign campaign)
        {
            await _context.Campaigns.AddAsync(campaign);
            await _context.SaveChangesAsync();
        }

        //6. Update campaign
        public async Task UpdateCampaignAsync(Campaign campaign)
        {
            _context.Entry(campaign).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        //7. Delete campaign
        public async Task DeleteCampaignAsync(Guid campaignId)
        {
            var vaccCampaign = await GetCampaignByIdAsync(campaignId);
            if (vaccCampaign != null)
            {
                _context.Campaigns.Remove(vaccCampaign);
                await _context.SaveChangesAsync();
            }
        }
    }
}
