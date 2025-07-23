using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccFormDto;


namespace SWP_SchoolMedicalManagementSystem_Service.Service.Interface
{
    public interface IConsentFormService
    {
        Task<List<ConsentFormResponse>> GetAllConsentFormsAsync();
        Task<ConsentFormResponse> GetConsentFormByIdAsync(Guid consentFormId);
        Task<List<ConsentFormResponse>> GetConsentFormsByCampaignIdAsync(Guid campaignId);
        Task<List<ConsentFormResponse>> GetConsentFormsByStudentIdAsync(Guid studentId);
        Task CreateConsentFormAsync(ConsentFormRequest consentForm);
        Task UpdateConsentFormAsync(Guid consentFormId, ConsentFormRequest consentForm);
        Task DeleteConsentFormAsync(Guid consentFormId);
    }
}
