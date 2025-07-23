using System;
using System.Collections.Generic;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.HealthRecordDto;
using SchoolMedicalManagementSystem.Enum;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.MockData
{
    public static class HealthRecordMockData
    {
        public static HealthRecord GetHealthRecordEntity()
        {
            return new HealthRecord
            {
                Id = Guid.NewGuid(),
                StudentId = Guid.NewGuid(),
                Height = "170",
                Weight = "60",
                BloodType = "O",
                Allergies = "Không",
                ChronicDiseases = "Không",
                PastMedicalHistory = "Không",
                VisionLeft = "10/10",
                VisionRight = "10/10",
                HearingLeft = "Bình thường",
                HearingRight = "Bình thường",
                VaccinationHistory = "Đầy đủ",
                OtherNotes = "Không"
            };
        }

        public static HealthRecordResponse GetHealthRecordResponseDto()
        {
            return new HealthRecordResponse
            {
                Id = Guid.NewGuid(),
                Height = "170",
                Weight = "60",
                BloodType = "O",
                Allergies = "Không",
                ChronicDiseases = "Không",
                PastMedicalHistory = "Không",
                VisionLeft = "10/10",
                VisionRight = "10/10",
                HearingLeft = "Bình thường",
                HearingRight = "Bình thường",
                VaccinationHistory = "Đầy đủ",
                OtherNotes = "Không"
            };
        }

        public static HealthRecordRequest GetHealthRecordRequestDto()
        {
            return new HealthRecordRequest
            {
                Height = "170",
                Weight = "60",
                BloodType = "O",
                Allergies = "Không",
                ChronicDiseases = "Không",
                PastMedicalHistory = "Không",
                VisionLeft = "10/10",
                VisionRight = "10/10",
                HearingLeft = "Bình thường",
                HearingRight = "Bình thường",
                VaccinationHistory = "Đầy đủ",
                OtherNotes = "Không"
            };
        }
    }
} 