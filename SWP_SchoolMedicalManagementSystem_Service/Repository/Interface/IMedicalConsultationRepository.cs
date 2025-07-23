using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_Service.Repository.Interface
{
    public interface IMedicalConsultationRepository
    {
        Task<List<MedicalConsultation>> GetAllMedicalConsultationsAsync();
        Task<MedicalConsultation?> GetMedicalConsultationByIdAsync(Guid id);
        Task CreateMedicalConsultationAsync(MedicalConsultation medicalConsultation);
        Task UpdateMedicalConsultationAsync(MedicalConsultation medicalConsultation);
        Task DeleteMedicalConsultationAsync(Guid id);
    }
}
