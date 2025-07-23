using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccResultDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_Service.Service.Interface
{
    public interface IVaccResultService
    {
        Task<List<VaccResultResponse>> GetAllVaccResultsAsync();
        Task<VaccResultResponse?> GetVaccResultByIdAsync(Guid vaccResultId);
        Task CreateVaccResultAsync(VaccResultRequest vaccResult);
        Task UpdateVaccResultAsync(Guid vaccResultId, VaccResultRequest vaccResult);
        Task DeleteVaccResultAsync(Guid vaccResultId);
    }
}
