using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;

namespace SWP_SchoolMedicalManagementSystem_Service.Repository.Interface
{
    public interface IMedicalDiaryRepository
    {
        Task<List<MedicalDiary>> GetAllMedicineDiary();
        Task<MedicalDiary?> GetMedicineDiaryById(Guid id);
        Task<List<MedicalDiary>> GetMedicineDiaryByStudentId(Guid studentId);
        Task CreateMedicineDiary(MedicalDiary medicineDiary);
        Task UpdateMedicineDiary(MedicalDiary medicineDiary);
        Task DeleteMedicineDiary(Guid id);
    }
}
