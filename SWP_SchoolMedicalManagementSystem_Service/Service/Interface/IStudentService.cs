using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.StudentDto;


namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Service
{
    public interface IStudentService
    {
        Task<List<StudentResponse>> GetAllStudentsAsync();
        Task<StudentResponse?> GetStudentByIdAsync(Guid studentId);
        Task<StudentResponse?> GetStudentByStudentCodeAsync(string studentCode);
        Task<List<StudentResponse>> GetStudentsByParentIdAsync(Guid parentId);
        Task<List<StudentResponse>> GetStudentsByClassAsync(string className);
        Task<List<StudentResponse>> GetStudentsBySchoolYearAsync(string schoolYear);
        Task CreateStudentAsync(StudentRequest student);
        Task UpdateStudentAsync(Guid studentId, StudentRequest student);
        Task DeleteStudentAsync(Guid studentId);
    }
}