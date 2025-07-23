using Microsoft.AspNetCore.Mvc;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.AuthDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.EmailDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.UserDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.PasswordResetDto;
using SWP_SchoolMedicalManagementSystem_Service.Extension;
using SWP_SchoolMedicalManagementSystem_Service.Service.Interface;

namespace SWP_SchoolMedicalManagementSystem_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUploadImage _uploadImageService;
        private readonly IEmailService _emailService;
        public AuthController(IUserService userService, IUploadImage uploadImage, IEmailService emailService)
        {
            _userService = userService;
            _uploadImageService = uploadImage;
            _emailService = emailService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequestDto loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest("Invalid login request.");
            }
            var response = await _userService.Login(loginRequest);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequestDto registerRequest)
        {
            if (registerRequest == null || string.IsNullOrEmpty(registerRequest.Username) || string.IsNullOrEmpty(registerRequest.Password))
            {
                return BadRequest("Invalid registration request.");
            }
            await _userService.Register(registerRequest);
            return Ok("Register!!");
        }

        [HttpPost("/image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }
            var result = await _uploadImageService.UploadImageAsync(file);
            if (result == null)
            {
                return BadRequest("Failed to upload image.");
            }
            return Ok(result);
        }

        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail([FromBody] EmailMedicalDiaryCreateDto emailRequest)
        {
            await _emailService.SendEmailAsync(emailRequest.Recipient, emailRequest.Body, emailRequest.Subject);
            return Ok("Email sent successfully.");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto dto)
        {
            await _userService.ForgotPasswordAsync(dto.Email);
            return Ok("OTP sent to your email.");
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequestDto dto)
        {
            var isValid = await _userService.VerifyOtpAsync(dto.Email, dto.Otp);
            return Ok(new { isValid });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto dto)
        {
            await _userService.ResetPasswordAsync(dto.Email, dto.Otp, dto.NewPassword);
            return Ok("Password reset successfully.");
        }
    }
}
