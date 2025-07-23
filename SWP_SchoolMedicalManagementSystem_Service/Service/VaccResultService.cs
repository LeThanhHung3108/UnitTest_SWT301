using AutoMapper;
using Microsoft.AspNetCore.Http;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccResultDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;

namespace SWP_SchoolMedicalManagementSystem_Service.Service
{
    public class VaccResultService : IVaccResultService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IVaccResultRepository _vaccResultRepository;
        private readonly IMapper _mapper;

        public VaccResultService(IHttpContextAccessor httpContextAccessor, IVaccResultRepository vaccResultRepository, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _vaccResultRepository = vaccResultRepository;
            _mapper = mapper;
        }

        //1. Get all vaccination results
        public async Task<List<VaccResultResponse>> GetAllVaccResultsAsync()
        {
            var vaccResults = await _vaccResultRepository.GetAllVaccResultsAsync();
            if (vaccResults == null || !vaccResults.Any())
                throw new KeyNotFoundException("No vaccination results found.");
            return _mapper.Map<List<VaccResultResponse>>(vaccResults);
        }

        //2. Get vaccination result by ID
        public async Task<VaccResultResponse?> GetVaccResultByIdAsync(Guid vaccResultId)
        {
            var vaccResult = await _vaccResultRepository.GetVaccResultByIdAsync(vaccResultId);
            if(vaccResult == null)
                throw new KeyNotFoundException($"Vaccination result with ID {vaccResultId} not found.");
            return _mapper.Map<VaccResultResponse>(vaccResult);
        }

        //3. Create a new vaccination result
        public async Task CreateVaccResultAsync(VaccResultRequest vaccResult)
        {
            var newVaccResult = _mapper.Map<VaccinationResult>(vaccResult);
            if (newVaccResult == null)
                throw new ArgumentNullException(nameof(vaccResult), "Vaccination result data is required.");
            newVaccResult.CreatedBy = GetCurrentUsername();
            newVaccResult.CreateAt = DateTime.UtcNow;
            newVaccResult.UpdateAt = DateTime.UtcNow;
            // Optionally set CreatedBy, UpdatedBy here if you have user context
            await _vaccResultRepository.CreateVaccResultAsync(newVaccResult);
        }

        //4. Update an existing vaccination result
        public async Task UpdateVaccResultAsync(Guid vaccResultId, VaccResultRequest vaccResult)
        {
            var existingVaccResult = await _vaccResultRepository.GetVaccResultByIdAsync(vaccResultId);
            if (existingVaccResult == null)
                throw new KeyNotFoundException($"Vaccination result with ID {vaccResultId} not found.");

            _mapper.Map(vaccResult, existingVaccResult);
            existingVaccResult.UpdatedBy = GetCurrentUsername();
            existingVaccResult.UpdateAt = DateTime.UtcNow;
            // Optionally set UpdatedBy here if you have user context
            await _vaccResultRepository.UpdateVaccResultAsync(existingVaccResult);
        }

        //5. Delete a vaccination result
        public async Task DeleteVaccResultAsync(Guid vaccResultId)
        {
            var existingVaccResult = await _vaccResultRepository.GetVaccResultByIdAsync(vaccResultId);
            if (existingVaccResult == null)
                throw new KeyNotFoundException($"Vaccination result with ID {vaccResultId} not found.");

            await _vaccResultRepository.DeleteVaccResultAsync(vaccResultId);
        }

        private string GetCurrentUsername()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirst("username")?.Value ?? "Unknown User";
        }
    }
}
