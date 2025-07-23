using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.AuthDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace SWP_SchoolMedicalManagementSystem_Service.Extension
{
    public class TokenGenerator : ITokenGeneratior
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public TokenGenerator(IConfiguration configuration, IMapper mapper, IUserRepository userRepository)
        {
            _configuration = configuration;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<AuthResponseDto> GenerateToken(User user, string role)
        {
            var accessTokenExpiration = int.Parse(_configuration["JWT:AccessTokenExpirationMinutes"]);
            var refreshTokenExpiration = int.Parse(_configuration["JWT:RefreshTokenExpirationDays"]);

            var claims = new List<Claim>
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("username", user.Username ?? string.Empty),
                new Claim("name", user.FullName ?? string.Empty),
                new Claim("email", user.Email ?? string.Empty),
                new Claim("role", role),
            };

            var keyString = _configuration["JWT:SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var accessToken = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(accessTokenExpiration),
                signingCredentials: creds
            );

            var accessTokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);

            var refreshTokenString = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            return new AuthResponseDto()
            {
                AccessToken = accessTokenString,
                RefreshToken = refreshTokenString,
            };
        }
    }
}
