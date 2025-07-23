using System;
using System.Collections.Generic;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccCampaignDto;
using SchoolMedicalManagementSystem.Enum;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.MockData
{
    public static class CampaignMockData
    {
        public static Campaign GetCampaignEntity()
        {
            return new Campaign
            {
                Id = Guid.NewGuid(),
                Name = "Chiến dịch tiêm chủng mùa xuân",
                Description = "Tiêm chủng cho học sinh toàn trường",
                Status = CampaignStatus.Planned,
                Type = CampaignType.Vaccination,
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
                Schedules = new List<Schedule>()
            };
        }

        public static CampaignResponse GetCampaignResponseDto()
        {
            return new CampaignResponse
            {
                Id = Guid.NewGuid(),
                Name = "Chiến dịch tiêm chủng mùa xuân",
                Description = "Tiêm chủng cho học sinh toàn trường",
                Status = CampaignStatus.Planned,
                Type = CampaignType.Vaccination,
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };
        }

        public static CampaignRequest GetCampaignRequestDto()
        {
            return new CampaignRequest
            {
                Name = "Chiến dịch tiêm chủng mùa xuân",
                Description = "Tiêm chủng cho học sinh toàn trường",
                Status = CampaignStatus.Planned,
                Type = CampaignType.Vaccination
            };
        }
    }
} 