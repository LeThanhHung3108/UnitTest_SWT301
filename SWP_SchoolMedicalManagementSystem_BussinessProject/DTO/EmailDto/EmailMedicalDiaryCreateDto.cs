using System.ComponentModel.DataAnnotations;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.EmailDto
{
    public class EmailMedicalDiaryCreateDto
    {
        public string Subject { get; set; } = string.Empty;
        public string Recipient { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
