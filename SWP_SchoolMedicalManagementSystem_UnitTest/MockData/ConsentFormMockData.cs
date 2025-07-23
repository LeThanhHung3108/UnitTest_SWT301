using System;
using System.Collections.Generic;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccFormDto;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.MockData
{
    public static class ConsentFormMockData
    {
        public static ConsentForm GetConsentFormEntity()
        {
            return new ConsentForm
            {
                Id = Guid.NewGuid(),
                CampaignId = Guid.NewGuid(),
                StudentId = Guid.NewGuid(),
                IsApproved = true,
                ConsentDate = DateTime.UtcNow,
                ReasonForDecline = null
            };
        }

        public static ConsentFormResponse GetConsentFormResponseDto()
        {
            return new ConsentFormResponse
            {
                Id = Guid.NewGuid(),
                CampaignId = Guid.NewGuid(),
                CampaignName = "Chiến dịch mùa xuân",
                StudentId = Guid.NewGuid(),
                StudentName = "Nguyễn Văn A",
                IsApproved = true,
                ConsentDate = DateTime.UtcNow,
                ReasonForDecline = null
            };
        }
    }
} 