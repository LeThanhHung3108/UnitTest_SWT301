using AutoMapper;
using Microsoft.AspNetCore.Http;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.HealthCheckupResultDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;

namespace SWP_SchoolMedicalManagementSystem_Service.Service
{
    public class HealthCheckupResultService : IHealthCheckupResultService
    {
        private readonly IHealthCheckupResultRepository _healthCheckupResultRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HealthCheckupResultService(IHealthCheckupResultRepository healthCheckupResultRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _healthCheckupResultRepository = healthCheckupResultRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetCurrentUserName()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst("username")?.Value ?? "Unknown User";
        }

        public async Task CreateHealthCheckupResult(HealthCheckupCreateRequestDto request)
        {
            var healthCheckupResult = _mapper.Map<HealthCheckupResult>(request);
            healthCheckupResult.CreateAt = DateTime.UtcNow;
            healthCheckupResult.UpdateAt = DateTime.UtcNow;
            healthCheckupResult.CreatedBy = GetCurrentUserName();

            await _healthCheckupResultRepository.Create(healthCheckupResult);
        }

        public async Task DeleteHealthCheckupResult(Guid id)
        {
            var healthCheckupResult = await _healthCheckupResultRepository.GetById(id);
            if (healthCheckupResult == null)
                throw new KeyNotFoundException($"Health checkup result with ID {id} not found.");

            await _healthCheckupResultRepository.Delete(id);
        }

        public async Task<List<HealthCheckupResponseDto>> GetAll()
        {
            var healthCheckupResults = await _healthCheckupResultRepository.GetAll();
            return _mapper.Map<List<HealthCheckupResponseDto>>(healthCheckupResults);
        }

        public async Task<HealthCheckupResponseDto> GetById(Guid id)
        {
            var healthCheckupResult = await _healthCheckupResultRepository.GetById(id);
            if (healthCheckupResult == null)
                throw new KeyNotFoundException($"Health checkup result with ID {id} not found.");

            return _mapper.Map<HealthCheckupResponseDto>(healthCheckupResult);
        }

        public async Task UpdateHealthCheckupResult(Guid requestId, HealthCheckupUpdateRequestDto request)
        {
            var healthCheckupResult = await _healthCheckupResultRepository.GetById(requestId);
            if (healthCheckupResult == null)
                throw new KeyNotFoundException($"Health checkup result with ID {requestId} not found.");

            _mapper.Map(request, healthCheckupResult);
            healthCheckupResult.UpdateAt = DateTime.UtcNow;
            healthCheckupResult.UpdatedBy = GetCurrentUserName();

            await _healthCheckupResultRepository.Update(healthCheckupResult);
        }
    }
}
