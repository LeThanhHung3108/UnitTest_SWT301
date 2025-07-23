using System;
using System.Collections.Generic;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalIncidentDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.StudentDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalSupplyUsageDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalSupplierDto;
using SchoolMedicalManagementSystem.Enum;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.MockData
{
    public static class MedicalIncidentMockData
    {
        public static MedicalIncident GetMedicalIncidentEntity()
        {
            return new MedicalIncident
            {
                Id = Guid.NewGuid(),
                StudentId = Guid.NewGuid(),
                MedicalStaffId = Guid.NewGuid(),
                IncidentType = IncidentType.Fever,
                IncidentDate = DateTime.UtcNow,
                Description = "Sốt cao 39 độ",
                ActionsTaken = "Đưa đi y tế, cho uống thuốc hạ sốt",
                Outcome = "Đã hạ sốt, về nhà nghỉ ngơi",
                Status = IncidentStatus.Resolved,
                MedicalSupplyUsages = new List<MedicalSupplyUsage>
                {
                    new MedicalSupplyUsage
                    {
                        SupplyId = Guid.NewGuid(),
                        IncidentId = Guid.NewGuid(),
                        QuantityUsed = 2,
                        Notes = "Dùng 2 viên paracetamol",
                        UsageDate = DateTime.UtcNow
                    }
                }
            };
        }

        public static IncidentResponseDto GetIncidentResponseDto()
        {
            return new IncidentResponseDto
            {
                Id = Guid.NewGuid(),
                Student = new StudentResponse
                {
                    Id = Guid.NewGuid(),
                    FullName = "Nguyễn Văn A",
                    StudentCode = "HS001"
                },
                MedicalStaffId = Guid.NewGuid(),
                MedicalStaffName = "Bác sĩ B",
                IncidentType = IncidentType.Fever,
                IncidentDate = DateTime.UtcNow,
                Description = "Sốt cao 39 độ",
                ActionsTaken = "Đưa đi y tế, cho uống thuốc hạ sốt",
                Outcome = "Đã hạ sốt, về nhà nghỉ ngơi",
                Status = IncidentStatus.Resolved,
                ParentNotified = true,
                ParentNotificationDate = DateTime.UtcNow,
                MedicalSupplyUsages = new List<MedicalSupplyUsageResponseDto>
                {
                    new MedicalSupplyUsageResponseDto
                    {
                        MedicalSupply = new SupplierResponseDto
                        {
                            Id = Guid.NewGuid(),
                            SupplyName = "Paracetamol",
                            SupplyType = SupplyType.Medicine,
                            Unit = "Viên",
                            Quantity = 100,
                            Supplier = "Nhà thuốc A"
                        },
                        QuantityUsed = 2,
                        UsageDate = DateTime.UtcNow,
                        Notes = "Dùng 2 viên paracetamol"
                    }
                }
            };
        }

        public static IncidentCreateRequestDto GetIncidentCreateRequestDto()
        {
            return new IncidentCreateRequestDto
            {
                StudentId = Guid.NewGuid(),
                IncidentType = IncidentType.Fever,
                IncidentDate = DateTime.UtcNow,
                Description = "Sốt cao 39 độ",
                ActionsTaken = "Đưa đi y tế, cho uống thuốc hạ sốt",
                Outcome = "Đã hạ sốt, về nhà nghỉ ngơi",
                Status = IncidentStatus.Resolved,
                MedicalSupplyUsage = new List<MedicalSupplyUsageCreateDto>
                {
                    new MedicalSupplyUsageCreateDto
                    {
                        MedicalSupplierId = Guid.NewGuid(),
                        QuantityUsed = 2,
                        Notes = "Dùng 2 viên paracetamol",
                        UsageDate = DateTime.UtcNow
                    }
                }
            };
        }
    }
} 