using Microsoft.EntityFrameworkCore;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Context;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Repository.Repository.Interface;

namespace SWP_SchoolMedicalManagementSystem_Repository.Repository
{
    public class ConsentFormRepository : IConsentFormRepository
    {
        private readonly ApplicationDBContext _context;

        public ConsentFormRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        //1. Get all consent forms
        public async Task<List<ConsentForm>> GetAllConsentFormsAsync()
        {
            return await _context.ConsentForms
                .Include(vf => vf.Campaign)
                .Include(vf => vf.Student)
                .Include(vf => vf.Campaign!.Schedules)
                .ToListAsync();
        }

        //2. Get consent form by ID
        public async Task<ConsentForm> GetConsentFormByIdAsync(Guid consentFormId)
        {
            return await _context.ConsentForms
                .Include(vf => vf.Campaign)
                .Include(vf => vf.Student)
                .Include(vf => vf.Campaign!.Schedules)
                .FirstOrDefaultAsync(vf => vf.Id == consentFormId);
        }

        //3. Get consent forms by campaign ID
        public async Task<List<ConsentForm>> GetConsentFormsByCampaignIdAsync(Guid campaignId)
        {
            return await _context.ConsentForms
                .Include(vf => vf.Campaign)
                .Include(vf => vf.Student)
                .Include(vf => vf.Campaign!.Schedules)
                .Where(vf => vf.CampaignId == campaignId)
                .ToListAsync();
        }

        //4. Get consent forms by student ID
        public async Task<List<ConsentForm>> GetConsentFormsByStudentIdAsync(Guid studentId)
        {
            return await _context.ConsentForms
                .Include(vf => vf.Campaign)
                .Include(vf => vf.Student)
                .Include(vf => vf.Campaign!.Schedules)
                .Where(vf => vf.StudentId == studentId)
                .ToListAsync();
        }

        //5. Create a new consent form
        public async Task CreateConsentFormAsync(ConsentForm consentForm)
        {
            await _context.ConsentForms.AddAsync(consentForm);
            await _context.SaveChangesAsync();
        }

        //6. Update an existing consent form
        public async Task UpdateConsentFormAsync(ConsentForm consentForm)
        {
            _context.Entry(consentForm).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        //7. Delete a consent form
        public async Task DeleteConsentFormAsync(Guid consentFormId)
        {
            var consentForm = await GetConsentFormByIdAsync(consentFormId);
            if (consentForm != null)
            {
                _context.ConsentForms.Remove(consentForm);
                await _context.SaveChangesAsync();
            }
        }
    }
}