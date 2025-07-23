using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices;

namespace SWP_SchoolMedicalManagementSystem_Service.Extension
{
    public class UploadImage : IUploadImage
    {
        private readonly Cloudinary _cloudinary;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _cloudinaryConfig;

        public UploadImage(IConfiguration configuration)
        {

            _configuration = configuration;
            _cloudinaryConfig = _configuration.GetSection("CloudinarySetting");

            var account = new Account
            {
                ApiKey = _cloudinaryConfig.GetSection("ApiKey").Value,
                ApiSecret = _cloudinaryConfig.GetSection("ApiSecret").Value,
                Cloud = _cloudinaryConfig.GetSection("CloudName").Value
            };

            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream)
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);

            }
            return uploadResult.SecureUrl.ToString();
        }
    }
}
