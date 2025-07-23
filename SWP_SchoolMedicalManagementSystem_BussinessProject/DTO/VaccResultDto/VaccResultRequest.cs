using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccResultDto
{
    public class VaccResultRequest
    {
        public Guid? ScheduleDetailId { get; set; }
        public string? DosageGiven { get; set; }
        public string? SideEffects { get; set; }
        public string? Notes { get; set; }
    }
}
