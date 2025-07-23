using System;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccResultDto;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.MockData
{
    public static class VaccResultMockData
    {
        public static VaccinationResult GetVaccinationResultEntity()
        {
            return new VaccinationResult
            {
                Id = Guid.NewGuid(),
                DosageGiven = "0.5ml",
                SideEffects = "Không",
                Notes = "Tiêm thành công",
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };
        }

        public static VaccResultResponse GetVaccResultResponseDto()
        {
            return new VaccResultResponse
            {
                Id = Guid.NewGuid(),
                DosageGiven = "0.5ml",
                SideEffects = "Không",
                Notes = "Tiêm thành công",
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };
        }
    }
} 