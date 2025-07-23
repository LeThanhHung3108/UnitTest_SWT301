namespace SWP_SchoolMedicalManagementSystem_Service.Extension
{
    public interface IEmailService
    {
        Task SendEmailAsync(string recipient, string body, string subject);
    }
}
