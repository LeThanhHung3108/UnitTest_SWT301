using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Entity
{
    public class PasswordReset
    {
         public Guid Id { get; set; }
         public string? Email { get; set; }
         public string? Otp { get; set; }
         public DateTime Expiration { get; set; }
         public bool IsUsed { get; set; }
        
    }
}
