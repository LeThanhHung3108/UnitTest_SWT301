using AutoMapper;
using Microsoft.AspNetCore.Http;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalIncidentDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;

namespace SWP_SchoolMedicalManagementSystem_Service.Service
{
    public class MedicalIncidentService : IMedicalIncidentService
    {
        private readonly IMedicalIncidentRepository _medicalIncidentRepository;
        private readonly IMedicalSupplierRepository _medicalSupplierRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MedicalIncidentService(IMedicalIncidentRepository medicalIncidentRepository, IMapper mapper,
            IHttpContextAccessor httpContextAccessor, IStudentRepository studentRepository,
            IMedicalSupplierRepository medicalSupplierRepository)
        {
            _medicalIncidentRepository = medicalIncidentRepository ?? throw new ArgumentNullException(nameof(medicalIncidentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpContextAccessor = httpContextAccessor;
            _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
            _medicalSupplierRepository = medicalSupplierRepository ?? throw new ArgumentNullException(nameof(medicalSupplierRepository));
        }

        private string GetCurrentUsername()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirst("username")?.Value ?? "Unknown User";
        }

        public async Task CreateIncidentAsync(IncidentCreateRequestDto incident)
        {
            var student = await _studentRepository.GetStudentByIdAsync(incident.StudentId);
            if (student == null)
                throw new KeyNotFoundException($"Student with ID {incident.StudentId} not found.");

            var newIncident = _mapper.Map<MedicalIncident>(incident);
            var listSupplier = new List<MedicalSupplyUsage>();

            foreach (var supplierList in incident.MedicalSupplyUsage)
            {
                var supplier = await _medicalSupplierRepository.GetSupplierByIdAsync(supplierList.MedicalSupplierId);
                if (supplier == null)
                    throw new KeyNotFoundException($"Supplier with ID {supplierList.MedicalSupplierId} not found.");

                listSupplier.Add(new MedicalSupplyUsage
                {
                    MedicalSupply = supplier,
                    QuantityUsed = supplierList.QuantityUsed,
                    Notes = supplierList.Notes,
                    UsageDate = supplierList.UsageDate,
                    SupplyId = supplierList.MedicalSupplierId
                });
            }

            newIncident.MedicalSupplyUsages = listSupplier;
            newIncident.Student = student;
            newIncident.CreateAt = DateTime.UtcNow;
            newIncident.CreatedBy = GetCurrentUsername();
            await _medicalIncidentRepository.CreateIncidentAsync(newIncident);
        }

        public async Task DeleteIncidentAsync(Guid id)
        {
            var oldIncident = await _medicalIncidentRepository.GetIncidentByIdAsync(id);
            if (oldIncident == null)
                throw new KeyNotFoundException($"Incident with ID {id} not found.");

            await _medicalIncidentRepository.DeleteIncidentAsync(id);
        }

        public async Task<List<IncidentResponseDto>> GetAllIncidentsAsync()
        {
            var incidents = await _medicalIncidentRepository.GetAllIncidentsAsync();
            if (incidents == null || !incidents.Any())
                throw new KeyNotFoundException("No incidents found.");

            return incidents.Select(incident => _mapper.Map<IncidentResponseDto>(incident)).ToList();
        }

        public async Task<IncidentResponseDto?> GetIncidentByIdAsync(Guid id)
        {
            var incident = await _medicalIncidentRepository.GetIncidentByIdAsync(id);
            if (incident == null)
                throw new KeyNotFoundException($"Incident with ID {id} not found.");

            return _mapper.Map<IncidentResponseDto>(incident);
        }

        public async Task<List<IncidentResponseDto>> GetIncidentsByStudentIdAsync(Guid studentId)
        {
            var incidents = await _medicalIncidentRepository.GetIncidentsByStudentIdAsync(studentId);
            if (incidents == null || !incidents.Any())
                throw new KeyNotFoundException($"No incidents found for student ID {studentId}.");

            return incidents.Select(incident => _mapper.Map<IncidentResponseDto>(incident)).ToList();
        }

        public async Task UpdateIncidentAsync(Guid id, IncidentUpdateRequestDto incident)
        {
            var existingIncident = await _medicalIncidentRepository.GetIncidentByIdAsync(id);
            if (existingIncident == null)
                throw new KeyNotFoundException($"Incident with ID {id} not found.");

            _mapper.Map(incident, existingIncident);

            existingIncident.UpdateAt = DateTime.UtcNow;
            existingIncident.UpdatedBy = GetCurrentUsername();
            await _medicalIncidentRepository.UpdateIncidentAsync(existingIncident);
        }
    }
}
