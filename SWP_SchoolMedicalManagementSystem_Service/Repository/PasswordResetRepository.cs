using Microsoft.EntityFrameworkCore;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Context;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.PasswordResetDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_Service.Repository
{
    public class PasswordResetRepository : IPasswordResetRepository
    {
        private readonly ApplicationDBContext _context;
        public PasswordResetRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task Add(PasswordReset passwordReset)
        {
            await _context.PasswordResets.AddAsync(passwordReset);
            await _context.SaveChangesAsync();
        }

        public async Task<PasswordReset> GetValidRequest(string email, string otp)
        {
            return await _context.PasswordResets
                .Where(pr => pr.Email == email && pr.Otp == otp && !pr.IsUsed && pr.Expiration > DateTime.UtcNow)
                .FirstOrDefaultAsync();
        }

        public async Task MarkAsUsed(Guid passwordResetId)
        {
            var passwordReset = await _context.PasswordResets.FindAsync(passwordResetId);
            if (passwordReset != null)
            {
                passwordReset.IsUsed = true;
                await _context.SaveChangesAsync();
            }   
        }
    }
}
