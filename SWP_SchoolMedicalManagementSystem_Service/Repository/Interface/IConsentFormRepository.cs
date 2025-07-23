using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;

namespace SWP_SchoolMedicalManagementSystem_Repository.Repository.Interface
{
    public interface IConsentFormRepository
    {
        Task<List<ConsentForm>> GetAllConsentFormsAsync();
        Task<ConsentForm> GetConsentFormByIdAsync(Guid consentFormId);
        Task<List<ConsentForm>> GetConsentFormsByCampaignIdAsync(Guid campaignId);
        Task<List<ConsentForm>> GetConsentFormsByStudentIdAsync(Guid studentId);
        Task CreateConsentFormAsync(ConsentForm consentForm);
        Task UpdateConsentFormAsync(ConsentForm consentForm);
        Task DeleteConsentFormAsync(Guid consentFormId);
    }
}