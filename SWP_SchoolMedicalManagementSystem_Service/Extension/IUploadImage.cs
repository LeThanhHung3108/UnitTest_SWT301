using Microsoft.AspNetCore.Http;

namespace SWP_SchoolMedicalManagementSystem_Service.Extension
{
    public interface IUploadImage
    {
        public Task<string> UploadImageAsync(IFormFile file);
    }
}
