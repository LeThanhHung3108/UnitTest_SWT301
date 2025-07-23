using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_Service.Repository.Interface
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllStudentsAsync();
        Task<Student?> GetStudentByIdAsync(Guid studentId);
        Task<Student?> GetStudentByStudentCodeAsync(string studentCode);
        Task<List<Student>> GetStudentsByParentIdAsync(Guid parentId);
        Task<List<Student>> GetStudentsByClassAsync(string className);
        Task<List<Student>> GetStudentsBySchoolYearAsync(string schoolYear);
        Task CreateStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(Guid studentId);
    }
}