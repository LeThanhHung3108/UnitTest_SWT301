using AutoMapper;
using Microsoft.AspNetCore.Http;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalConsultationDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;


namespace SWP_SchoolMedicalManagementSystem_Service.Service
{
    public class MedicalConsultationService : IMedicalConsultationService
    {

        private readonly IMedicalConsultationRepository _medicalConsultationRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public MedicalConsultationService(IMedicalConsultationRepository medicalConsultationRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _medicalConsultationRepository = medicalConsultationRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task CreateMedicalConsultationAsync(MedicalConsultationCreateRequestDto medicalConsultation)
        {
            await _medicalConsultationRepository.CreateMedicalConsultationAsync(_mapper.Map<MedicalConsultation>(medicalConsultation));
        }

        public async Task DeleteMedicalConsultationAsync(Guid id)
        {
            var medicalConsultation = await _medicalConsultationRepository.GetMedicalConsultationByIdAsync(id);
            if (medicalConsultation == null)
            {
                throw new KeyNotFoundException($"Medical consultation with ID {id} not found.");
            }
            await _medicalConsultationRepository.DeleteMedicalConsultationAsync(id);
        }

        public async Task<List<MedicalConsultationResponeDto>> GetAllMedicalConsultationsAsync()
        {
            var medicalConsultations = await _medicalConsultationRepository.GetAllMedicalConsultationsAsync();
            return _mapper.Map<List<MedicalConsultationResponeDto>>(medicalConsultations);

        }

        public async Task<MedicalConsultationResponeDto> GetMedicalConsultationByIdAsync(Guid id)
        {
            var medicalConsultation = await _medicalConsultationRepository.GetMedicalConsultationByIdAsync(id);
            return _mapper.Map<MedicalConsultationResponeDto>(medicalConsultation);
        }

        public async Task UpdateMedicalConsultationAsync(Guid id, MedicalConsultationUpdateRequesteDto medicalConsultation)
        {
            var existingConsultation = await _medicalConsultationRepository.GetMedicalConsultationByIdAsync(id);
            if (existingConsultation == null)
            {
                throw new KeyNotFoundException($"Medical consultation with ID {id} not found.");
            }
            _mapper.Map(medicalConsultation, existingConsultation);
            existingConsultation.UpdateAt = DateTime.UtcNow;
            await _medicalConsultationRepository.UpdateMedicalConsultationAsync(existingConsultation);
        }
    }
}
