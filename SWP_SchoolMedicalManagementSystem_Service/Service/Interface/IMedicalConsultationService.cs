using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalConsultationDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_Service.Service.Interface
{
    public interface IMedicalConsultationService
    {
        Task<List<MedicalConsultationResponeDto>> GetAllMedicalConsultationsAsync();
        Task<MedicalConsultationResponeDto> GetMedicalConsultationByIdAsync(Guid id);
        Task CreateMedicalConsultationAsync(MedicalConsultationCreateRequestDto medicalConsultation);
        Task UpdateMedicalConsultationAsync(Guid id,MedicalConsultationUpdateRequesteDto medicalConsultation);
        Task DeleteMedicalConsultationAsync(Guid id);
    }
}
