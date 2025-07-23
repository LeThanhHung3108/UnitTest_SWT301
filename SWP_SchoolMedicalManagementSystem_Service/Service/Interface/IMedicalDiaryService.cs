using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalDiaryDto;

namespace SWP_SchoolMedicalManagementSystem_Service.Service.Interface
{
    public interface IMedicalDiaryService
    {
        Task<List<MedicalDiaryResponseDto>> GetAllMedicineDiary();
        Task<MedicalDiaryResponseDto?> GetMedicineDiaryById(Guid id);
        Task<List<MedicalDiaryResponseDto>> GetMedicineDiaryByStudentId(Guid studentId);
        Task CreateMedicineDiary(MedicalDiaryRequestDto request);
        Task UpdateMedicineDiary(Guid diaryId, MedicalDiaryRequestDto request);
        Task DeleteMedicineDiary(Guid id);
    }
}
