using System;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.StudentDto;
using SchoolMedicalManagementSystem.Enum;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.MockData
{
    public static class StudentMockData
    {
        public static Student GetStudentEntity()
        {
            return new Student
            {
                Id = Guid.NewGuid(),
                StudentCode = "HS001",
                FullName = "Nguyễn Văn A",
                DateOfBirth = new DateTime(2010, 1, 1),
                Gender = Gender.Male,
                Class = "5A",
                SchoolYear = "2024-2025"
            };
        }

        public static StudentResponse GetStudentResponseDto()
        {
            return new StudentResponse
            {
                Id = Guid.NewGuid(),
                StudentCode = "HS001",
                FullName = "Nguyễn Văn A",
                DateOfBirth = new DateTime(2010, 1, 1),
                Gender = Gender.Male,
                Class = "5A",
                SchoolYear = "2024-2025"
            };
        }
    }
} 