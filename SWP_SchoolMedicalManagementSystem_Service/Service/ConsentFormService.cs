using AutoMapper;
using Microsoft.AspNetCore.Http;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccFormDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Repository.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;

namespace SWP_SchoolMedicalManagementSystem_Service.Service
{
    public class ConsentFormService : IConsentFormService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConsentFormRepository _consentFormRepository;
        private readonly IMapper _mapper;

        public ConsentFormService(
            IHttpContextAccessor httpContextAccessor,
            IConsentFormRepository consentFormRepository,
            IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _consentFormRepository = consentFormRepository;
            _mapper = mapper;
        }

        public async Task<List<ConsentFormResponse>> GetAllConsentFormsAsync()
        {
            var consentForms = await _consentFormRepository.GetAllConsentFormsAsync();
            return _mapper.Map<List<ConsentFormResponse>>(consentForms);
        }

        public async Task<ConsentFormResponse> GetConsentFormByIdAsync(Guid consentFormId)
        {
            var consentForm = await _consentFormRepository.GetConsentFormByIdAsync(consentFormId);
            if (consentForm == null)
                throw new KeyNotFoundException($"Consent form with ID {consentFormId} not found.");
            return _mapper.Map<ConsentFormResponse>(consentForm);
        }

        public async Task<List<ConsentFormResponse>> GetConsentFormsByCampaignIdAsync(Guid campaignId)
        {
            var consentForms = await _consentFormRepository.GetConsentFormsByCampaignIdAsync(campaignId);
            return _mapper.Map<List<ConsentFormResponse>>(consentForms);
        }

        public async Task<List<ConsentFormResponse>> GetConsentFormsByStudentIdAsync(Guid studentId)
        {
            var consentForms = await _consentFormRepository.GetConsentFormsByStudentIdAsync(studentId);
            return _mapper.Map<List<ConsentFormResponse>>(consentForms);
        }

        public async Task CreateConsentFormAsync(ConsentFormRequest consentForm)
        {
            var newConsentForm = _mapper.Map<ConsentForm>(consentForm);
            newConsentForm.CreatedBy = GetCurrentUsername();
            newConsentForm.CreateAt = DateTime.UtcNow;
            await _consentFormRepository.CreateConsentFormAsync(newConsentForm);
        }

        public async Task UpdateConsentFormAsync(Guid consentFormId, ConsentFormRequest consentForm)
        {
            var existingConsentForm = await _consentFormRepository.GetConsentFormByIdAsync(consentFormId);
            if (existingConsentForm == null)
                throw new KeyNotFoundException($"Consent form with ID {consentFormId} not found.");
            _mapper.Map(consentForm, existingConsentForm);
            existingConsentForm.UpdatedBy = GetCurrentUsername();
            existingConsentForm.UpdateAt = DateTime.UtcNow;
            await _consentFormRepository.UpdateConsentFormAsync(existingConsentForm);
        }

        public async Task DeleteConsentFormAsync(Guid consentFormId)
        {
            var existingConsentForm = await _consentFormRepository.GetConsentFormByIdAsync(consentFormId);
            if (existingConsentForm == null)
                throw new KeyNotFoundException($"Consent form with ID {consentFormId} not found.");
            await _consentFormRepository.DeleteConsentFormAsync(consentFormId);
        }

        private string GetCurrentUsername()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirst("username")?.Value ?? "Unknown User";
        }
    }
}
