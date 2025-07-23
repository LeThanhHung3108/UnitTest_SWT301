using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccResultDto
{
    public class VaccResultResponse
    {
        public Guid Id { get; set; }
        public string? DosageGiven { get; set; }
        public string? SideEffects { get; set; }
        public string? Notes { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
