using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_Service.Repository.Interface
{
    public interface IVaccResultRepository
    {
        Task<List<VaccinationResult>> GetAllVaccResultsAsync();
        Task<VaccinationResult?> GetVaccResultByIdAsync(Guid vaccResultId);
        Task CreateVaccResultAsync(VaccinationResult vaccResult);
        Task UpdateVaccResultAsync(VaccinationResult vaccResult);
        Task DeleteVaccResultAsync(Guid vaccResultId);
    }
}
