using System;
using System.Collections.Generic;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.UserDto;
using SchoolMedicalManagementSystem.Enum;

namespace SWP_SchoolMedicalManagementSystem_UnitTest.MockData
{
    public static class UserMockData
    {
        public static User GetUserEntity()
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                FullName = "Quản trị viên",
                Email = "admin@example.com",
                UserRole = UserRole.Admin
            };
        }

        public static UserResponseDto GetUserResponseDto()
        {
            return new UserResponseDto
            {
                Id = Guid.NewGuid().ToString(),
                Username = "admin",
                FullName = "Quản trị viên",
                Email = "admin@example.com",
                UserRole = UserRole.Admin
            };
        }
    }
} 