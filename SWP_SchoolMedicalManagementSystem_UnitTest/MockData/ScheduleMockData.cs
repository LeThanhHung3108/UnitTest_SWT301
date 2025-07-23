using System;
using System.Collections.Generic;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccScheduleDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.ScheduleDto;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.MockData
{
    public static class ScheduleMockData
    {
        public static Schedule GetScheduleEntity()
        {
            return new Schedule
            {
                Id = Guid.NewGuid(),
                CampaignId = Guid.NewGuid(),
                ScheduledDate = DateTime.UtcNow,
                Location = "Phòng 101",
                Notes = "Lịch tiêm chủng",
                ScheduleDetails = new List<ScheduleDetail>()
            };
        }

        public static ScheduleResponse GetScheduleResponseDto()
        {
            return new ScheduleResponse
            {
                Id = Guid.NewGuid(),
                CampaignId = Guid.NewGuid(),
                ScheduledDate = DateTime.UtcNow,
                Location = "Phòng 101",
                Notes = "Lịch tiêm chủng"
            };
        }

        public static ScheduleGetByIdResponse GetScheduleGetByIdResponseDto()
        {
            return new ScheduleGetByIdResponse
            {
                Id = Guid.NewGuid(),
                CampaignId = Guid.NewGuid(),
                ScheduledDate = DateTime.UtcNow,
                Location = "Phòng 101",
                Notes = "Lịch tiêm chủng"
            };
        }
    }
} 